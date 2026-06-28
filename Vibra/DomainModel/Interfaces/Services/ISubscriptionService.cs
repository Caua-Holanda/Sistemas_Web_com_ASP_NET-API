using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DTOs.Subscription;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface ISubscriptionService
    {
        Task<SubscriptionResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<SubscriptionResponseDto>> GetByUserIdAsync(Guid userId);
        Task<SubscriptionResponseDto> CreateAsync(SubscriptionCreateDto createDto);
        Task<SubscriptionResponseDto> UpdateAsync(Guid id, SubscriptionUpdateDto updateDto);
        Task DeleteAsync(Guid id);
        Task CancelSubscriptionAsync(Guid id);
    }
}