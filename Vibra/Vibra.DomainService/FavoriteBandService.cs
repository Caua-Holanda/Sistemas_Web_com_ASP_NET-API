using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.FavoriteBand;

namespace Vibra.DomainService
{
    public class FavoriteBandService : IFavoriteBandService
    {
        private readonly IRepository<FavoriteBand> _favoriteBandRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FavoriteBandService(IRepository<FavoriteBand> favoriteBandRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _favoriteBandRepo = favoriteBandRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FavoriteBandResponseDto>> GetByUserIdAsync(Guid userId)
        {
            var favorites = await _favoriteBandRepo.ReadAll();
            var filtered = favorites.Where(f => f.UserId == userId);
            return _mapper.Map<IEnumerable<FavoriteBandResponseDto>>(filtered);
        }

        public async Task<IEnumerable<FavoriteBandResponseDto>> GetByBandIdAsync(Guid bandId)
        {
            var favorites = await _favoriteBandRepo.ReadAll();
            var filtered = favorites.Where(f => f.BandId == bandId);
            return _mapper.Map<IEnumerable<FavoriteBandResponseDto>>(filtered);
        }

        public async Task FavoriteAsync(FavoriteBandCreateDto createDto)
        {
            var favorites = await _favoriteBandRepo.ReadAll();
            if (favorites.Any(f => f.UserId == createDto.UserId && f.BandId == createDto.BandId)) return;

            var favorite = new FavoriteBand
            {
                UserId = createDto.UserId,
                BandId = createDto.BandId,
                CreatedAt = DateTime.UtcNow
            };
            _favoriteBandRepo.Create(favorite);
            await _unitOfWork.CommitAsync();
        }

        public async Task UnfavoriteAsync(Guid userId, Guid bandId)
        {
            var favorites = await _favoriteBandRepo.ReadAll();
            var favorite = favorites.FirstOrDefault(f => f.UserId == userId && f.BandId == bandId);
            if (favorite != null)
            {
                _favoriteBandRepo.Delete(favorite);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
