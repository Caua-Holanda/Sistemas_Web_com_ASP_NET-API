using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.FavoriteBand;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteBandController : ControllerBase
    {
        private readonly IFavoriteBandService _favoriteBandService;

        public FavoriteBandController(IFavoriteBandService favoriteBandService)
        {
            _favoriteBandService = favoriteBandService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<FavoriteBandResponseDto>>> GetByUserId(Guid userId)
        {
            var favorites = await _favoriteBandService.GetByUserIdAsync(userId);
            return Ok(favorites);
        }

        [HttpGet("band/{bandId}")]
        public async Task<ActionResult<IEnumerable<FavoriteBandResponseDto>>> GetByBandId(Guid bandId)
        {
            var favorites = await _favoriteBandService.GetByBandIdAsync(bandId);
            return Ok(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> Favorite([FromBody] FavoriteBandCreateDto createDto)
        {
            await _favoriteBandService.FavoriteAsync(createDto);
            return Ok();
        }

        [HttpDelete("unfavorite")]
        public async Task<IActionResult> Unfavorite([FromQuery] Guid userId, [FromQuery] Guid bandId)
        {
            await _favoriteBandService.UnfavoriteAsync(userId, bandId);
            return NoContent();
        }
    }
}