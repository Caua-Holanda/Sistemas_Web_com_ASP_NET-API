using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DTOs.FavoriteSong;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface IFavoriteSongService
    {
        Task<IEnumerable<FavoriteSongResponseDto>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<FavoriteSongResponseDto>> GetBySongIdAsync(Guid songId);
        Task FavoriteAsync(FavoriteSongCreateDto createDto);
        Task UnfavoriteAsync(Guid userId, Guid songId);
    }
}