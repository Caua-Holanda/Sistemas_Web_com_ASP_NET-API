using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Playlist;
using Vibra.DTOs.PlaylistSong;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistResponseDto>> GetById(Guid id)
        {
            var playlist = await _playlistService.GetByIdAsync(id);
            if (playlist == null)
                return NotFound();
            return Ok(playlist);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PlaylistResponseDto>>> GetByUserId(Guid userId)
        {
            var playlists = await _playlistService.GetByUserIdAsync(userId);
            return Ok(playlists);
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistResponseDto>> Create([FromBody] PlaylistCreateDto createDto)
        {
            var created = await _playlistService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PlaylistResponseDto>> Update(Guid id, [FromBody] PlaylistUpdateDto updateDto)
        {
            var updated = await _playlistService.UpdateAsync(id, updateDto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _playlistService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{playlistId}/songs")]
        public async Task<IActionResult> AddSong([FromRoute] Guid playlistId, [FromBody] PlaylistSongCreateDto createDto)
        {
            // Ensure the PlaylistId in DTO matches route
            if (createDto.PlaylistId != playlistId)
                return BadRequest("PlaylistId in body does not match route.");
            await _playlistService.AddSongToPlaylistAsync(createDto);
            return Ok();
        }

        [HttpDelete("{playlistId}/songs/{songId}")]
        public async Task<IActionResult> RemoveSong(Guid playlistId, Guid songId)
        {
            await _playlistService.RemoveSongFromPlaylistAsync(playlistId, songId);
            return NoContent();
        }
    }
}