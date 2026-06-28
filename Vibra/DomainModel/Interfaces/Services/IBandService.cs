using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DTOs.Band;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface IBandService
    {
        Task<BandResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<BandResponseDto>> GetAllAsync();
        Task<IEnumerable<BandResponseDto>> SearchByNameAsync(string name);
        Task<BandResponseDto> CreateAsync(BandCreateDto createDto);
        Task<BandResponseDto> UpdateAsync(Guid id, BandUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}