using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.FavoriteSong;

namespace Vibra.DomainService
{
    public class FavoriteSongService : IFavoriteSongService
    {
        private readonly IRepository<FavoriteSong> _favoriteSongRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FavoriteSongService(IRepository<FavoriteSong> favoriteSongRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _favoriteSongRepo = favoriteSongRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FavoriteSongResponseDto>> GetByUserIdAsync(Guid userId)
        {
            var favorites = await _favoriteSongRepo.ReadAll();
            var filtered = favorites.Where(f => f.UserId == userId);
            return _mapper.Map<IEnumerable<FavoriteSongResponseDto>>(filtered);
        }

        public async Task<IEnumerable<FavoriteSongResponseDto>> GetBySongIdAsync(Guid songId)
        {
            var favorites = await _favoriteSongRepo.ReadAll();
            var filtered = favorites.Where(f => f.SongId == songId);
            return _mapper.Map<IEnumerable<FavoriteSongResponseDto>>(filtered);
        }

        public async Task FavoriteAsync(FavoriteSongCreateDto createDto)
        {
            var favorites = await _favoriteSongRepo.ReadAll();
            if (favorites.Any(f => f.UserId == createDto.UserId && f.SongId == createDto.SongId)) return;

            var favorite = new FavoriteSong
            {
                UserId = createDto.UserId,
                SongId = createDto.SongId,
                CreatedAt = DateTime.UtcNow
            };
            _favoriteSongRepo.Create(favorite);
            await _unitOfWork.CommitAsync();
        }

        public async Task UnfavoriteAsync(Guid userId, Guid songId)
        {
            var favorites = await _favoriteSongRepo.ReadAll();
            var favorite = favorites.FirstOrDefault(f => f.UserId == userId && f.SongId == songId);
            if (favorite != null)
            {
                _favoriteSongRepo.Delete(favorite);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
