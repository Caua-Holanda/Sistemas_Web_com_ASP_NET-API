using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Playlist;
using Vibra.DTOs.PlaylistSong;
using Vibra.DTOs.Song;

namespace Vibra.DomainService
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IRepository<Playlist> _playlistRepo;
        private readonly IRepository<PlaylistSong> _playlistSongRepo;
        private readonly IRepository<Song> _songRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlaylistService(
            IRepository<Playlist> playlistRepo,
            IRepository<PlaylistSong> playlistSongRepo,
            IRepository<Song> songRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _playlistRepo = playlistRepo;
            _playlistSongRepo = playlistSongRepo;
            _songRepo = songRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PlaylistResponseDto> GetByIdAsync(Guid id)
        {
            var playlist = await _playlistRepo.Read(id);
            if (playlist == null) return null;

            return await BuildPlaylistResponseAsync(playlist);
        }

        public async Task<IEnumerable<PlaylistResponseDto>> GetByUserIdAsync(Guid userId)
        {
            var playlists = await _playlistRepo.ReadAll();
            var filtered = playlists
                .Where(p => p.UserId == userId && p.DeletedAt == null)
                .OrderBy(p => p.Name)
                .ToList();

            var response = new List<PlaylistResponseDto>();

            foreach (var playlist in filtered)
                response.Add(await BuildPlaylistResponseAsync(playlist));

            return response;
        }

        public async Task<PlaylistResponseDto> CreateAsync(PlaylistCreateDto createDto)
        {
            var playlist = new Playlist
            {
                Id = Guid.NewGuid(),
                Name = createDto.Name,
                UserId = createDto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            _playlistRepo.Create(playlist);
            await _unitOfWork.CommitAsync();
            return await BuildPlaylistResponseAsync(playlist);
        }

        public async Task<PlaylistResponseDto> UpdateAsync(Guid id, PlaylistUpdateDto updateDto)
        {
            var playlist = await _playlistRepo.Read(id);
            if (playlist == null) return null;

            playlist.Name = updateDto.Name;
            playlist.UpdatedAt = DateTime.UtcNow;

            _playlistRepo.Update(playlist);
            await _unitOfWork.CommitAsync();
            return await BuildPlaylistResponseAsync(playlist);
        }

        public async Task DeleteAsync(Guid id)
        {
            var playlist = await _playlistRepo.Read(id);
            if (playlist != null)
            {
                playlist.DeletedAt = DateTime.UtcNow;
                _playlistRepo.Update(playlist);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task AddSongToPlaylistAsync(PlaylistSongCreateDto createDto)
        {
            var playlist = await _playlistRepo.Read(createDto.PlaylistId);
            if (playlist == null || playlist.DeletedAt != null)
                throw new Exception("Playlist não encontrada.");

            var song = await _songRepo.Read(createDto.SongId);
            if (song == null || song.DeletedAt != null)
                throw new Exception("Música não encontrada.");

            var playlistSongs = await _playlistSongRepo.ReadAll();
            var exists = playlistSongs.Any(p =>
                p.PlaylistId == createDto.PlaylistId &&
                p.SongId == createDto.SongId);

            if (exists) return;

            var nextOrder = createDto.Order;
            if (nextOrder <= 0)
            {
                var currentSongs = playlistSongs.Where(p => p.PlaylistId == createDto.PlaylistId).ToList();
                nextOrder = currentSongs.Any() ? currentSongs.Max(p => p.Order) + 1 : 1;
            }

            var playlistSong = new PlaylistSong
            {
                PlaylistId = createDto.PlaylistId,
                SongId = createDto.SongId,
                Order = nextOrder
            };

            _playlistSongRepo.Create(playlistSong);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveSongFromPlaylistAsync(Guid playlistId, Guid songId)
        {
            var playlistSongs = await _playlistSongRepo.ReadAll();
            var playlistSong = playlistSongs.FirstOrDefault(p =>
                p.PlaylistId == playlistId &&
                p.SongId == songId);

            if (playlistSong != null)
            {
                _playlistSongRepo.Delete(playlistSong);
                await _unitOfWork.CommitAsync();
            }
        }

        private async Task<PlaylistResponseDto> BuildPlaylistResponseAsync(Playlist playlist)
        {
            var playlistSongs = await _playlistSongRepo.ReadAll();
            var songs = await _songRepo.ReadAll();

            var items = playlistSongs
                .Where(p => p.PlaylistId == playlist.Id)
                .OrderBy(p => p.Order)
                .Select(p =>
                {
                    var song = songs.FirstOrDefault(s => s.Id == p.SongId);
                    return new PlaylistSongResponseDto
                    {
                        PlaylistId = p.PlaylistId,
                        SongId = p.SongId,
                        Order = p.Order,
                        Song = song == null ? null : _mapper.Map<SongResponseDto>(song)
                    };
                })
                .ToList();

            return new PlaylistResponseDto
            {
                Id = playlist.Id,
                Name = playlist.Name,
                CreatedAt = playlist.CreatedAt,
                UpdatedAt = playlist.UpdatedAt,
                UserId = playlist.UserId,
                PlaylistSongs = items
            };
        }
    }
}
