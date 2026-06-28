using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DTOs.Plan;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface IPlanService
    {
        Task<PlanResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<PlanResponseDto>> GetAllAsync();
        Task<PlanResponseDto> CreateAsync(PlanCreateDto createDto);
        Task<PlanResponseDto> UpdateAsync(Guid id, PlanUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}