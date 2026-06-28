using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Band;

namespace Vibra.DomainService
{
    public class BandService : IBandService
    {
        private readonly IRepository<Band> _bandRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BandService(IRepository<Band> bandRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _bandRepo = bandRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BandResponseDto> GetByIdAsync(Guid id)
        {
            var band = await _bandRepo.Read(id);
            return _mapper.Map<BandResponseDto>(band);
        }

        public async Task<IEnumerable<BandResponseDto>> GetAllAsync()
        {
            var bands = await _bandRepo.ReadAll();
            return _mapper.Map<IEnumerable<BandResponseDto>>(bands);
        }

        public async Task<IEnumerable<BandResponseDto>> SearchByNameAsync(string name)
        {
            var bands = await _bandRepo.ReadAll();
            var filtered = bands.Where(b => b.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            return _mapper.Map<IEnumerable<BandResponseDto>>(filtered);
        }

        public async Task<BandResponseDto> CreateAsync(BandCreateDto createDto)
        {
            var band = _mapper.Map<Band>(createDto);
            band.Id = Guid.NewGuid();
            band.CreatedAt = DateTime.UtcNow;
            _bandRepo.Create(band);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<BandResponseDto>(band);
        }

        public async Task<BandResponseDto> UpdateAsync(Guid id, BandUpdateDto updateDto)
        {
            var band = await _bandRepo.Read(id);
            if (band == null) return null;

            _mapper.Map(updateDto, band);
            band.UpdatedAt = DateTime.UtcNow;
            _bandRepo.Update(band);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<BandResponseDto>(band);
        }

        public async Task DeleteAsync(Guid id)
        {
            var band = await _bandRepo.Read(id);
            if (band != null)
            {
                band.DeletedAt = DateTime.UtcNow;
                _bandRepo.Update(band);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}