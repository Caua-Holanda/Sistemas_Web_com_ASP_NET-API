using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Card;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardResponseDto>> GetById(Guid id)
        {
            var card = await _cardService.GetByIdAsync(id);
            if (card == null)
                return NotFound();
            return Ok(card);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CardResponseDto>>> GetByUserId(Guid userId)
        {
            var cards = await _cardService.GetByUserIdAsync(userId);
            return Ok(cards);
        }

        [HttpPost]
        public async Task<ActionResult<CardResponseDto>> Create([FromBody] CardCreateDto createDto)
        {
            var created = await _cardService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CardResponseDto>> Update(Guid id, [FromBody] CardUpdateDto updateDto)
        {
            var updated = await _cardService.UpdateAsync(id, updateDto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cardService.DeleteAsync(id);
            return NoContent();
        }
    }
}