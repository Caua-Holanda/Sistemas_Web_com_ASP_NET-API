using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DTOs.User;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> CreateAsync(UserCreateDto createDto);
        Task<UserResponseDto> UpdateAsync(Guid id, UserUpdateDto updateDto);
        Task DeleteAsync(Guid id);
        Task<UserResponseDto> AuthenticateAsync(string email, string password);
    }
}