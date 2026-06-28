using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.FavoriteSong;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteSongController : ControllerBase
    {
        private readonly IFavoriteSongService _favoriteSongService;

        public FavoriteSongController(IFavoriteSongService favoriteSongService)
        {
            _favoriteSongService = favoriteSongService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<FavoriteSongResponseDto>>> GetByUserId(Guid userId)
        {
            var favorites = await _favoriteSongService.GetByUserIdAsync(userId);
            return Ok(favorites);
        }

        [HttpGet("song/{songId}")]
        public async Task<ActionResult<IEnumerable<FavoriteSongResponseDto>>> GetBySongId(Guid songId)
        {
            var favorites = await _favoriteSongService.GetBySongIdAsync(songId);
            return Ok(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> Favorite([FromBody] FavoriteSongCreateDto createDto)
        {
            await _favoriteSongService.FavoriteAsync(createDto);
            return Ok();
        }

        [HttpDelete("unfavorite")]
        public async Task<IActionResult> Unfavorite([FromQuery] Guid userId, [FromQuery] Guid songId)
        {
            await _favoriteSongService.UnfavoriteAsync(userId, songId);
            return NoContent();
        }
    }
}