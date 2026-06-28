using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Transaction;

namespace Vibra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionResponseDto>> GetById(Guid id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);
            if (transaction == null)
                return NotFound();
            return Ok(transaction);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TransactionResponseDto>>> GetByUserId(Guid userId)
        {
            var transactions = await _transactionService.GetByUserIdAsync(userId);
            return Ok(transactions);
        }

        [HttpPost("authorize")]
        public async Task<ActionResult<TransactionResponseDto>> Authorize([FromBody] TransactionCreateDto createDto)
        {
            var result = await _transactionService.AuthorizeTransactionAsync(createDto);
            return Ok(result);
        }
    }
}