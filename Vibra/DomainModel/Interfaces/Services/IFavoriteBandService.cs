using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DTOs.FavoriteBand;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface IFavoriteBandService
    {
        Task<IEnumerable<FavoriteBandResponseDto>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<FavoriteBandResponseDto>> GetByBandIdAsync(Guid bandId);
        Task FavoriteAsync(FavoriteBandCreateDto createDto);
        Task UnfavoriteAsync(Guid userId, Guid bandId);
    }
}