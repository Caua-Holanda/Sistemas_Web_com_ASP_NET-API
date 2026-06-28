using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Plan;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanResponseDto>>> GetAll()
        {
            var plans = await _planService.GetAllAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanResponseDto>> GetById(Guid id)
        {
            var plan = await _planService.GetByIdAsync(id);
            if (plan == null)
                return NotFound();
            return Ok(plan);
        }

        [HttpPost]
        public async Task<ActionResult<PlanResponseDto>> Create([FromBody] PlanCreateDto createDto)
        {
            var created = await _planService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PlanResponseDto>> Update(Guid id, [FromBody] PlanUpdateDto updateDto)
        {
            var updated = await _planService.UpdateAsync(id, updateDto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _planService.DeleteAsync(id);
            return NoContent();
        }
    }
}