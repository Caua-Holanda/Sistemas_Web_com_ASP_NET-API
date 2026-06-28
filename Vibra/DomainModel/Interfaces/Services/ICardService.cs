using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vibra.DTOs.Card;

namespace Vibra.DomainModel.Interfaces.Services
{
    public interface ICardService
    {
        Task<CardResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<CardResponseDto>> GetByUserIdAsync(Guid userId);
        Task<CardResponseDto> CreateAsync(CardCreateDto createDto);
        Task<CardResponseDto> UpdateAsync(Guid id, CardUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}