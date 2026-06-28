using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DTOs.FavoriteSong;
using Vibra.DTOs.Song;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface ISongService
    {
        Task<SongResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<SongResponseDto>> GetAllAsync();
        Task<IEnumerable<SongResponseDto>> GetByAlbumIdAsync(Guid albumId);
        Task<IEnumerable<SongResponseDto>> SearchByTitleAsync(string title);
        Task<SongResponseDto> CreateAsync(SongCreateDto createDto);
        Task<SongResponseDto> UpdateAsync(Guid id, SongUpdateDto updateDto);
        Task DeleteAsync(Guid id);
        Task FavoriteSongAsync(FavoriteSongCreateDto createDto);
        Task UnfavoriteSongAsync(Guid userId, Guid songId);
    }
}