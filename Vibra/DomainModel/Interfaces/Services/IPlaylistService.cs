using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DTOs.Playlist;
using Vibra.DTOs.PlaylistSong;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface IPlaylistService
    {
        Task<PlaylistResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<PlaylistResponseDto>> GetByUserIdAsync(Guid userId);
        Task<PlaylistResponseDto> CreateAsync(PlaylistCreateDto createDto);
        Task<PlaylistResponseDto> UpdateAsync(Guid id, PlaylistUpdateDto updateDto);
        Task DeleteAsync(Guid id);
        Task AddSongToPlaylistAsync(PlaylistSongCreateDto createDto);
        Task RemoveSongFromPlaylistAsync(Guid playlistId, Guid songId);
    }
}