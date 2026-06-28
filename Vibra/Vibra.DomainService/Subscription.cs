using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Enums;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Subscription;

namespace Vibra.DomainService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepository<Subscription> _subscriptionRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscriptionService(IRepository<Subscription> subscriptionRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _subscriptionRepo = subscriptionRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubscriptionResponseDto> GetByIdAsync(Guid id)
        {
            var subscription = await _subscriptionRepo.Read(id);
            return _mapper.Map<SubscriptionResponseDto>(subscription);
        }

        public async Task<IEnumerable<SubscriptionResponseDto>> GetByUserIdAsync(Guid userId)
        {
            var subscriptions = await _subscriptionRepo.ReadAll();
            var filtered = subscriptions.Where(s => s.UserId == userId);
            return _mapper.Map<IEnumerable<SubscriptionResponseDto>>(filtered);
        }

        public async Task<SubscriptionResponseDto> CreateAsync(SubscriptionCreateDto createDto)
        {
            var subscription = _mapper.Map<Subscription>(createDto);
            subscription.Id = Guid.NewGuid();
            subscription.UserId = createDto.UserId;
            subscription.PlanId = createDto.PlanId;
            subscription.CreatedAt = DateTime.UtcNow;
            _subscriptionRepo.Create(subscription);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<SubscriptionResponseDto>(subscription);
        }

        public async Task<SubscriptionResponseDto> UpdateAsync(Guid id, SubscriptionUpdateDto updateDto)
        {
            var subscription = await _subscriptionRepo.Read(id);
            if (subscription == null) return null;

            _mapper.Map(updateDto, subscription);
            subscription.UpdatedAt = DateTime.UtcNow;
            _subscriptionRepo.Update(subscription);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<SubscriptionResponseDto>(subscription);
        }

        public async Task DeleteAsync(Guid id)
        {
            var subscription = await _subscriptionRepo.Read(id);
            if (subscription != null)
            {
                subscription.DeletedAt = DateTime.UtcNow;
                _subscriptionRepo.Update(subscription);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task CancelSubscriptionAsync(Guid id)
        {
            var subscription = await _subscriptionRepo.Read(id);
            if (subscription != null)
            {
                subscription.Status = SubscriptionStatus.Canceled;
                subscription.EndDate = DateTime.UtcNow;
                subscription.UpdatedAt = DateTime.UtcNow;
                _subscriptionRepo.Update(subscription);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
