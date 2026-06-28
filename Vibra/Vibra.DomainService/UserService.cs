using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Enums;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.User;

namespace Vibra.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Account> _accountRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepo, IRepository<Account> accountRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepo = userRepo;
            _accountRepo = accountRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepo.Read(id);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepo.ReadAll();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto> CreateAsync(UserCreateDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));
            if (string.IsNullOrWhiteSpace(createDto.Name)) throw new Exception("Informe o nome do usuário.");
            if (string.IsNullOrWhiteSpace(createDto.Email)) throw new Exception("Informe o e-mail do usuário.");
            if (string.IsNullOrWhiteSpace(createDto.Password)) throw new Exception("Informe a senha do usuário.");

            var exists = (await _userRepo.ReadAll()).Any(u => u.Email == createDto.Email);
            if (exists) throw new Exception("Já existe um usuário cadastrado com este e-mail.");

            var user = _mapper.Map<User>(createDto);
            user.Id = Guid.NewGuid();
            user.Name = createDto.Name.Trim();
            user.Email = createDto.Email.Trim();
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createDto.Password);
            user.CreatedAt = DateTime.UtcNow;
            _userRepo.Create(user);

            var account = new Account
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
                Limit = 100,
                Status = AccountStatus.Active,
                Balance = 100,
            };
            _accountRepo.Create(account);

            await _unitOfWork.CommitAsync();
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> UpdateAsync(Guid id, UserUpdateDto updateDto)
        {
            var user = await _userRepo.Read(id);
            if (user == null) return null;

            _mapper.Map(updateDto, user);
            user.UpdatedAt = DateTime.UtcNow;
            _userRepo.Update(user);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepo.Read(id);
            if (user != null)
            {
                user.DeletedAt = DateTime.UtcNow;
                _userRepo.Update(user);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<UserResponseDto> AuthenticateAsync(string email, string password)
        {
            var users = await _userRepo.ReadAll();
            var userEntity = users.FirstOrDefault(u => u.Email == email);
            if (userEntity == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, userEntity.PasswordHash)) return null;
            return _mapper.Map<UserResponseDto>(userEntity);
        }
    }
}
