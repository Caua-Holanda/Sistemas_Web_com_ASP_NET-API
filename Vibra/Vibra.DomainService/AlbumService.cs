using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProvaMed.DomainModel.Interfaces.UoW;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Interfaces.Repositories;
using Vibra.DomainModel.Interfaces.Services;
using Vibra.DTOs.Album;

namespace Vibra.DomainService
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AlbumService(IRepository<Album> albumRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _albumRepo = albumRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AlbumResponseDto> GetByIdAsync(Guid id)
        {
            var album = await _albumRepo.Read(id);
            return _mapper.Map<AlbumResponseDto>(album);
        }

        public async Task<IEnumerable<AlbumResponseDto>> GetAllAsync()
        {
            var albums = await _albumRepo.ReadAll();
            return _mapper.Map<IEnumerable<AlbumResponseDto>>(albums);
        }

        public async Task<IEnumerable<AlbumResponseDto>> GetByBandIdAsync(Guid bandId)
        {
            var albums = await _albumRepo.ReadAll();
            var filtered = albums.Where(a => a.BandId == bandId);
            return _mapper.Map<IEnumerable<AlbumResponseDto>>(filtered);
        }

        public async Task<IEnumerable<AlbumResponseDto>> SearchByTitleAsync(string title)
        {
            var albums = await _albumRepo.ReadAll();
            var filtered = albums.Where(a => a.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            return _mapper.Map<IEnumerable<AlbumResponseDto>>(filtered);
        }

        public async Task<AlbumResponseDto> CreateAsync(AlbumCreateDto createDto)
        {
            var album = _mapper.Map<Album>(createDto);
            album.Id = Guid.NewGuid();
            album.CreatedAt = DateTime.UtcNow;
            album.BandId = createDto.BandId;
            _albumRepo.Create(album);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<AlbumResponseDto>(album);
        }

        public async Task<AlbumResponseDto> UpdateAsync(Guid id, AlbumUpdateDto updateDto)
        {
            var album = await _albumRepo.Read(id);
            if (album == null) return null;

            _mapper.Map(updateDto, album);
            album.UpdatedAt = DateTime.UtcNow;
            _albumRepo.Update(album);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<AlbumResponseDto>(album);
        }

        public async Task DeleteAsync(Guid id)
        {
            var album = await _albumRepo.Read(id);
            if (album != null)
            {
                album.DeletedAt = DateTime.UtcNow;
                _albumRepo.Update(album);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
