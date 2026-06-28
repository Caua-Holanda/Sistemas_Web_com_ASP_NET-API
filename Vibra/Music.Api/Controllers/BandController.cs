using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Band;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandController : ControllerBase
    {
        private readonly IBandService _bandService;

        public BandController(IBandService bandService)
        {
            _bandService = bandService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BandResponseDto>>> GetAll()
        {
            var bands = await _bandService.GetAllAsync();
            return Ok(bands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BandResponseDto>> GetById(Guid id)
        {
            var band = await _bandService.GetByIdAsync(id);
            if (band == null)
                return NotFound();
            return Ok(band);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BandResponseDto>>> SearchByName([FromQuery] string name)
        {
            var bands = await _bandService.SearchByNameAsync(name);
            return Ok(bands);
        }

        [HttpPost]
        public async Task<ActionResult<BandResponseDto>> Create([FromBody] BandCreateDto createDto)
        {
            var created = await _bandService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BandResponseDto>> Update(Guid id, [FromBody] BandUpdateDto updateDto)
        {
            var updated = await _bandService.UpdateAsync(id, updateDto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bandService.DeleteAsync(id);
            return NoContent();
        }
    }
}