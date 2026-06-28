using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Plan;

namespace Vibra.DomainService
{
    public class PlanService : IPlanService
    {
        private readonly IRepository<Plan> _planRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanService(IRepository<Plan> planRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _planRepo = planRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PlanResponseDto> GetByIdAsync(Guid id)
        {
            var plan = await _planRepo.Read(id);
            return _mapper.Map<PlanResponseDto>(plan);
        }

        public async Task<IEnumerable<PlanResponseDto>> GetAllAsync()
        {
            var plans = await _planRepo.ReadAll();
            return _mapper.Map<IEnumerable<PlanResponseDto>>(plans);
        }

        public async Task<PlanResponseDto> CreateAsync(PlanCreateDto createDto)
        {
            var plan = _mapper.Map<Plan>(createDto);
            plan.Id = Guid.NewGuid();
            plan.CreatedAt = DateTime.UtcNow;
            _planRepo.Create(plan);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<PlanResponseDto>(plan);
        }

        public async Task<PlanResponseDto> UpdateAsync(Guid id, PlanUpdateDto updateDto)
        {
            var plan = await _planRepo.Read(id);
            if (plan == null) return null;

            _mapper.Map(updateDto, plan);
            plan.UpdatedAt = DateTime.UtcNow;
            _planRepo.Update(plan);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<PlanResponseDto>(plan);
        }

        public async Task DeleteAsync(Guid id)
        {
            var plan = await _planRepo.Read(id);
            if (plan != null)
            {
                plan.DeletedAt = DateTime.UtcNow;
                _planRepo.Update(plan);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}