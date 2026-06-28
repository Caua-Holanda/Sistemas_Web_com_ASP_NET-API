using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DTOs.Album;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface IAlbumService
    {
        Task<AlbumResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<AlbumResponseDto>> GetAllAsync();
        Task<IEnumerable<AlbumResponseDto>> GetByBandIdAsync(Guid bandId);
        Task<IEnumerable<AlbumResponseDto>> SearchByTitleAsync(string title);
        Task<AlbumResponseDto> CreateAsync(AlbumCreateDto createDto);
        Task<AlbumResponseDto> UpdateAsync(Guid id, AlbumUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}