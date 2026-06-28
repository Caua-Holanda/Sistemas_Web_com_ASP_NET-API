using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.FavoriteSong;
using Vibra.DTOs.Song;

namespace Vibra.DomainService
{
    public class SongService : ISongService
    {
        private readonly IRepository<Song> _songRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<FavoriteSong> _favoriteSongRepo;

        public SongService(
            IRepository<Song> songRepo,
            IRepository<FavoriteSong> favoriteSongRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _songRepo = songRepo;
            _favoriteSongRepo = favoriteSongRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SongResponseDto> GetByIdAsync(Guid id)
        {
            var song = await _songRepo.Read(id);
            return _mapper.Map<SongResponseDto>(song);
        }

        public async Task<IEnumerable<SongResponseDto>> GetAllAsync()
        {
            var songs = await _songRepo.ReadAll();
            return _mapper.Map<IEnumerable<SongResponseDto>>(songs);
        }

        public async Task<IEnumerable<SongResponseDto>> GetByAlbumIdAsync(Guid albumId)
        {
            var songs = await _songRepo.ReadAll();
            var filtered = songs.Where(s => s.AlbumId == albumId);
            return _mapper.Map<IEnumerable<SongResponseDto>>(filtered);
        }

        public async Task<IEnumerable<SongResponseDto>> SearchByTitleAsync(string title)
        {
            var songs = await _songRepo.ReadAll();
            var filtered = songs.Where(s => s.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            return _mapper.Map<IEnumerable<SongResponseDto>>(filtered);
        }

        public async Task<SongResponseDto> CreateAsync(SongCreateDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.AudioUrl)) throw new Exception("Informe a URL do áudio.");
            var song = _mapper.Map<Song>(createDto);
            song.Id = Guid.NewGuid();
            song.AlbumId = createDto.AlbumId;
            _songRepo.Create(song);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<SongResponseDto>(song);
        }

        public async Task<SongResponseDto> UpdateAsync(Guid id, SongUpdateDto updateDto)
        {
            var song = await _songRepo.Read(id);
            if (song == null) return null;

            _mapper.Map(updateDto, song);
            song.UpdatedAt = DateTime.UtcNow;
            _songRepo.Update(song);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<SongResponseDto>(song);
        }

        public async Task DeleteAsync(Guid id)
        {
            var song = await _songRepo.Read(id);
            if (song != null)
            {
                song.DeletedAt = DateTime.UtcNow;
                _songRepo.Update(song);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task FavoriteSongAsync(FavoriteSongCreateDto createDto)
        {
            var favorites = await _favoriteSongRepo.ReadAll();

            var alreadyExists = favorites.Any(f =>
                f.UserId == createDto.UserId &&
                f.SongId == createDto.SongId);

            if (alreadyExists)
                return;

            var favorite = new FavoriteSong
            {
                UserId = createDto.UserId,
                SongId = createDto.SongId,
                CreatedAt = DateTime.UtcNow
            };

            _favoriteSongRepo.Create(favorite);
            await _unitOfWork.CommitAsync();
        }

        public async Task UnfavoriteSongAsync(Guid userId, Guid songId)
        {
            var favorites = await _favoriteSongRepo.ReadAll();

            var favorite = favorites.FirstOrDefault(f =>
                f.UserId == userId &&
                f.SongId == songId);

            if (favorite == null)
                return;

            _favoriteSongRepo.Delete(favorite);
            await _unitOfWork.CommitAsync();
        }

    }
}
