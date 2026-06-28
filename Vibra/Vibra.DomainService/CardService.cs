using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Card;

namespace Vibra.DomainService
{
    public class CardService : ICardService
    {
        private readonly IRepository<Card> _cardRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CardService(IRepository<Card> cardRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _cardRepo = cardRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CardResponseDto> GetByIdAsync(Guid id)
        {
            var card = await _cardRepo.Read(id);
            return _mapper.Map<CardResponseDto>(card);
        }

        public async Task<IEnumerable<CardResponseDto>> GetByUserIdAsync(Guid userId)
        {
            var cards = await _cardRepo.ReadAll();
            var filtered = cards.Where(c => c.UserId == userId && c.DeletedAt == null);
            return _mapper.Map<IEnumerable<CardResponseDto>>(filtered);
        }

        public async Task<CardResponseDto> CreateAsync(CardCreateDto createDto)
        {
            var card = _mapper.Map<Card>(createDto);
            card.Id = Guid.NewGuid();
            card.UserId = createDto.UserId;
            card.ExpiryDate = NormalizeExpiryDate(createDto.ExpiryDate);
            _cardRepo.Create(card);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CardResponseDto>(card);
        }

        public async Task<CardResponseDto> UpdateAsync(Guid id, CardUpdateDto updateDto)
        {
            var card = await _cardRepo.Read(id);
            if (card == null) return null;

            _mapper.Map(updateDto, card);
            card.ExpiryDate = NormalizeExpiryDate(card.ExpiryDate);
            card.UpdatedAt = DateTime.UtcNow;
            _cardRepo.Update(card);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CardResponseDto>(card);
        }

        public async Task DeleteAsync(Guid id)
        {
            var card = await _cardRepo.Read(id);
            if (card != null)
            {
                card.DeletedAt = DateTime.UtcNow;
                _cardRepo.Update(card);
                await _unitOfWork.CommitAsync();
            }
        }

        private static string NormalizeExpiryDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;
            value = value.Trim();
            if (value.Length >= 7 && value[4] == '-') return value.Substring(5, 2) + "/" + value.Substring(0, 4);
            if (value.Length >= 10 && value[2] == '/') return value.Substring(3, 2) + "/" + value.Substring(6, 4);
            if (value.Length == 5 && value[2] == '/') return value.Substring(0, 3) + "20" + value.Substring(3, 2);
            return value.Length > 7 ? value.Substring(0, 7) : value;
        }
    }
}
