using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Account;

namespace Vibra.DomainService
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IRepository<Account> accountRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AccountResponseDto> GetByUserIdAsync(Guid userId)
        {
            var accounts = await _accountRepo.ReadAll();
            var account = accounts.FirstOrDefault(a => a.UserId == userId);
            return _mapper.Map<AccountResponseDto>(account);
        }

        public async Task<AccountResponseDto> UpdateAsync(Guid userId, AccountUpdateDto updateDto)
        {
            var accounts = await _accountRepo.ReadAll();
            var account = accounts.FirstOrDefault(a => a.UserId == userId);
            if (account == null) return null;

            _mapper.Map(updateDto, account);
            account.UpdatedAt = DateTime.UtcNow;
            _accountRepo.Update(account);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<AccountResponseDto>(account);
        }
    }
}