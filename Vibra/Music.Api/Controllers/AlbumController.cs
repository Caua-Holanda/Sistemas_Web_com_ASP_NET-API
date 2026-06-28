using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Album;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumResponseDto>>> GetAll()
        {
            var albums = await _albumService.GetAllAsync();
            return Ok(albums);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumResponseDto>> GetById(Guid id)
        {
            var album = await _albumService.GetByIdAsync(id);
            if (album == null)
                return NotFound();
            return Ok(album);
        }

        [HttpGet("band/{bandId}")]
        public async Task<ActionResult<IEnumerable<AlbumResponseDto>>> GetByBandId(Guid bandId)
        {
            var albums = await _albumService.GetByBandIdAsync(bandId);
            return Ok(albums);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AlbumResponseDto>>> SearchByTitle([FromQuery] string title)
        {
            var albums = await _albumService.SearchByTitleAsync(title);
            return Ok(albums);
        }

        [HttpPost]
        public async Task<ActionResult<AlbumResponseDto>> Create([FromBody] AlbumCreateDto createDto)
        {
            var created = await _albumService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AlbumResponseDto>> Update(Guid id, [FromBody] AlbumUpdateDto updateDto)
        {
            var updated = await _albumService.UpdateAsync(id, updateDto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _albumService.DeleteAsync(id);
            return NoContent();
        }
    }
}