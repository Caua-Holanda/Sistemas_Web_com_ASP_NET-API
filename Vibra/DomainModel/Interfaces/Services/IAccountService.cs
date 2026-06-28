using System;
using System.Threading.Tasks;
using Vibra.DTOs.Account;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AccountResponseDto> GetByUserIdAsync(Guid userId);
        Task<AccountResponseDto> UpdateAsync(Guid userId, AccountUpdateDto updateDto);
    }
}