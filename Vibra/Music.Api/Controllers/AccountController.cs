using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Account;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await _accountService.GetByUserIdAsync(userId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut("user/{userId}")]
        public async Task<IActionResult> Update(Guid userId, [FromBody] AccountUpdateDto updateDto)
        {
            var result = await _accountService.UpdateAsync(userId, updateDto);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}