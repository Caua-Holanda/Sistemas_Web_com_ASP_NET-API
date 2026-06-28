using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Subscription;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionResponseDto>> GetById(Guid id)
        {
            var subscription = await _subscriptionService.GetByIdAsync(id);
            if (subscription == null)
                return NotFound();
            return Ok(subscription);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<SubscriptionResponseDto>>> GetByUserId(Guid userId)
        {
            var subscriptions = await _subscriptionService.GetByUserIdAsync(userId);
            return Ok(subscriptions);
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionResponseDto>> Create([FromBody] SubscriptionCreateDto createDto)
        {
            var created = await _subscriptionService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SubscriptionResponseDto>> Update(Guid id, [FromBody] SubscriptionUpdateDto updateDto)
        {
            var updated = await _subscriptionService.UpdateAsync(id, updateDto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _subscriptionService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _subscriptionService.CancelSubscriptionAsync(id);
            return Ok();
        }
    }
}