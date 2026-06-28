using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DomainModel.Entities;
using Vibra.DTOs.Transaction;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<TransactionResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<TransactionResponseDto>> GetByUserIdAsync(Guid userId);
        Task<TransactionResponseDto> AuthorizeTransactionAsync(TransactionCreateDto createDto);
        Task NotifyMerchantAndUserAsync(Transaction transaction);
    }
}