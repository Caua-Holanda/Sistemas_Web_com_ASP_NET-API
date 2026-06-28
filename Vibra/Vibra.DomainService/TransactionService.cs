using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Enums;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Account;
using Vibra.DTOs.Transaction;

namespace Vibra.DomainService
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepo;
        private readonly IAccountService _accountService;
        private readonly IRepository<Card> _cardRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IRepository<Transaction> transactionRepo, IAccountService accountService, IRepository<Card> cardRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _transactionRepo = transactionRepo;
            _accountService = accountService;
            _cardRepo = cardRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionResponseDto> GetByIdAsync(Guid id)
        {
            var transaction = await _transactionRepo.Read(id);
            return _mapper.Map<TransactionResponseDto>(transaction);
        }

        public async Task<IEnumerable<TransactionResponseDto>> GetByUserIdAsync(Guid userId)
        {
            var transactions = await _transactionRepo.ReadAll();
            var filtered = transactions.Where(t => t.UserId == userId);
            return _mapper.Map<IEnumerable<TransactionResponseDto>>(filtered);
        }

        public async Task<TransactionResponseDto> AuthorizeTransactionAsync(TransactionCreateDto createDto)
        {
            var card = await _cardRepo.Read(createDto.CardId);
            if (card == null || card.UserId != createDto.UserId)
                return await CreateAndSaveDeniedTransaction(createDto, "Card not found for this user.");

            var account = await _accountService.GetByUserIdAsync(createDto.UserId);
            if (account == null || account.Status != AccountStatus.Active)
                return await CreateAndSaveDeniedTransaction(createDto, "Account is not active.");

            if (account.Balance + account.Limit < createDto.Amount)
                return await CreateAndSaveDeniedTransaction(createDto, "Insufficient funds or credit limit.");

            var lastTx = (await _transactionRepo.ReadAll())
                .Where(t => t.UserId == createDto.UserId && t.Status == TransactionStatus.Authorized)
                .OrderByDescending(t => t.Timestamp)
                .FirstOrDefault();

            if (lastTx != null && (DateTime.UtcNow - lastTx.Timestamp).TotalSeconds < 30)
                return await CreateAndSaveDeniedTransaction(createDto, "Too frequent transactions.");

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                UserId = createDto.UserId,
                CardId = createDto.CardId,
                Merchant = createDto.Merchant,
                Amount = createDto.Amount,
                Timestamp = DateTime.UtcNow,
                Status = TransactionStatus.Authorized,
                LastAuthorization = DateTime.UtcNow
            };

            _transactionRepo.Create(transaction);
            await _accountService.UpdateAsync(createDto.UserId, new AccountUpdateDto { Balance = account.Balance - createDto.Amount, Limit = account.Limit, Status = account.Status });
            await _unitOfWork.CommitAsync();

            await NotifyMerchantAndUserAsync(transaction);

            return _mapper.Map<TransactionResponseDto>(transaction);
        }

        private async Task<TransactionResponseDto> CreateAndSaveDeniedTransaction(TransactionCreateDto createDto, string reason)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                UserId = createDto.UserId,
                CardId = createDto.CardId,
                Merchant = createDto.Merchant,
                Amount = createDto.Amount,
                Timestamp = DateTime.UtcNow,
                Status = TransactionStatus.Denied,
                DenialReason = reason
            };
            _transactionRepo.Create(transaction);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<TransactionResponseDto>(transaction);
        }

        public async Task NotifyMerchantAndUserAsync(Transaction transaction)
        {
            Console.WriteLine($"Notifying merchant {transaction.Merchant} about transaction {transaction.Id}.");
            Console.WriteLine($"Notifying user {transaction.UserId} about transaction {transaction.Id}.");
            await Task.CompletedTask;
        }
    }
}
