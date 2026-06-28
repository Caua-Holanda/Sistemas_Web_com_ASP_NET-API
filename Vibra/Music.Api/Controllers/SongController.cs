using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.FavoriteSong;
using Vibra.DTOs.Song;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongResponseDto>>> GetAll()
        {
            var songs = await _songService.GetAllAsync();
            return Ok(songs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SongResponseDto>> GetById(Guid id)
        {
            var song = await _songService.GetByIdAsync(id);
            if (song == null)
                return NotFound();
            return Ok(song);
        }

        [HttpGet("album/{albumId}")]
        public async Task<ActionResult<IEnumerable<SongResponseDto>>> GetByAlbumId(Guid albumId)
        {
            var songs = await _songService.GetByAlbumIdAsync(albumId);
            return Ok(songs);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SongResponseDto>>> SearchByTitle([FromQuery] string title)
        {
            var songs = await _songService.SearchByTitleAsync(title);
            return Ok(songs);
        }

        [HttpPost]
        public async Task<ActionResult<SongResponseDto>> Create([FromBody] SongCreateDto createDto)
        {
            var created = await _songService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SongResponseDto>> Update(Guid id, [FromBody] SongUpdateDto updateDto)
        {
            var updated = await _songService.UpdateAsync(id, updateDto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _songService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("favorite")]
        public async Task<IActionResult> Favorite([FromBody] FavoriteSongCreateDto createDto)
        {
            await _songService.FavoriteSongAsync(createDto);
            return Ok();
        }

        [HttpDelete("unfavorite")]
        public async Task<IActionResult> Unfavorite([FromQuery] Guid userId, [FromQuery] Guid songId)
        {
            await _songService.UnfavoriteSongAsync(userId, songId);
            return NoContent();
        }
    }
}