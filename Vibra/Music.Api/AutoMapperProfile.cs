using AutoMapper;
using Vibra.DomainModel.Entities;
using Vibra.DTOs.Account;
using Vibra.DTOs.Album;
using Vibra.DTOs.Band;
using Vibra.DTOs.Card;
using Vibra.DTOs.FavoriteBand;
using Vibra.DTOs.FavoriteSong;
using Vibra.DTOs.Plan;
using Vibra.DTOs.Playlist;
using Vibra.DTOs.PlaylistSong;
using Vibra.DTOs.Song;
using Vibra.DTOs.Subscription;
using Vibra.DTOs.Transaction;
using Vibra.DTOs.User;

namespace Vibra
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User mappings
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.Cards, opt => opt.Ignore())
                .ForMember(dest => dest.Subscriptions, opt => opt.Ignore())
                .ForMember(dest => dest.Playlists, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteSongs, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteBands, opt => opt.Ignore())
                .ForMember(dest => dest.Transactions, opt => opt.Ignore());

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.Cards, opt => opt.Ignore())
                .ForMember(dest => dest.Subscriptions, opt => opt.Ignore())
                .ForMember(dest => dest.Playlists, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteSongs, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteBands, opt => opt.Ignore())
                .ForMember(dest => dest.Transactions, opt => opt.Ignore());

            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
                .ForMember(dest => dest.Cards, opt => opt.MapFrom(src => src.Cards))
                .ForMember(dest => dest.Subscriptions, opt => opt.MapFrom(src => src.Subscriptions))
                .ForMember(dest => dest.Playlists, opt => opt.MapFrom(src => src.Playlists))
                .ForMember(dest => dest.FavoriteSongs, opt => opt.MapFrom(src => src.FavoriteSongs))
                .ForMember(dest => dest.FavoriteBands, opt => opt.MapFrom(src => src.FavoriteBands))
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions));

            // Account mappings
            CreateMap<AccountCreateDto, Account>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<AccountUpdateDto, Account>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Account, AccountResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            // Card mappings
            CreateMap<CardCreateDto, Card>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<CardUpdateDto, Card>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Card, CardResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            // Plan mappings
            CreateMap<PlanCreateDto, Plan>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Subscriptions, opt => opt.Ignore());

            CreateMap<PlanUpdateDto, Plan>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Subscriptions, opt => opt.Ignore());

            CreateMap<Plan, PlanResponseDto>();

            // Subscription mappings
            CreateMap<SubscriptionCreateDto, Subscription>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Plan, opt => opt.Ignore());

            CreateMap<SubscriptionUpdateDto, Subscription>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Plan, opt => opt.Ignore());

            CreateMap<Subscription, SubscriptionResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.PlanId));

            // Band mappings
            CreateMap<BandCreateDto, Band>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Albums, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteBands, opt => opt.Ignore());

            CreateMap<BandUpdateDto, Band>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Albums, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteBands, opt => opt.Ignore());

            CreateMap<Band, BandResponseDto>();

            // Album mappings
            CreateMap<AlbumCreateDto, Album>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Band, opt => opt.Ignore())
                .ForMember(dest => dest.Songs, opt => opt.Ignore());

            CreateMap<AlbumUpdateDto, Album>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Band, opt => opt.Ignore())
                .ForMember(dest => dest.Songs, opt => opt.Ignore());

            CreateMap<Album, AlbumResponseDto>()
                .ForMember(dest => dest.BandId, opt => opt.MapFrom(src => src.BandId));

            // Song mappings
            CreateMap<SongCreateDto, Song>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Album, opt => opt.Ignore())
                .ForMember(dest => dest.PlaylistSongs, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteSongs, opt => opt.Ignore());

            CreateMap<SongUpdateDto, Song>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Album, opt => opt.Ignore())
                .ForMember(dest => dest.PlaylistSongs, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteSongs, opt => opt.Ignore());

            CreateMap<Song, SongResponseDto>()
                .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.AlbumId));

            // Playlist mappings
            CreateMap<PlaylistCreateDto, Playlist>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.PlaylistSongs, opt => opt.Ignore());

            CreateMap<PlaylistUpdateDto, Playlist>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.PlaylistSongs, opt => opt.Ignore());

            CreateMap<Playlist, PlaylistResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            // PlaylistSong mappings
            CreateMap<PlaylistSongCreateDto, PlaylistSong>()
                .ForMember(dest => dest.Playlist, opt => opt.Ignore())
                .ForMember(dest => dest.Song, opt => opt.Ignore());

            CreateMap<PlaylistSong, PlaylistSongResponseDto>()
                .ForMember(dest => dest.PlaylistId, opt => opt.MapFrom(src => src.PlaylistId))
                .ForMember(dest => dest.SongId, opt => opt.MapFrom(src => src.SongId))
                .ForMember(dest => dest.Song, opt => opt.MapFrom(src => src.Song));

            // FavoriteSong mappings
            CreateMap<FavoriteSongCreateDto, FavoriteSong>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Song, opt => opt.Ignore());

            CreateMap<FavoriteSong, FavoriteSongResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.SongId, opt => opt.MapFrom(src => src.SongId))
                .ForMember(dest => dest.Song, opt => opt.MapFrom(src => src.Song));

            // FavoriteBand mappings
            CreateMap<FavoriteBandCreateDto, FavoriteBand>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Band, opt => opt.Ignore());

            CreateMap<FavoriteBand, FavoriteBandResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.BandId, opt => opt.MapFrom(src => src.BandId))
                .ForMember(dest => dest.Band, opt => opt.MapFrom(src => src.Band));

            // Transaction mappings
            CreateMap<TransactionCreateDto, Transaction>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Timestamp, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.DenialReason, opt => opt.Ignore())
                .ForMember(dest => dest.LastAuthorization, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Card, opt => opt.Ignore());

            CreateMap<Transaction, TransactionResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CardId, opt => opt.MapFrom(src => src.CardId));
        }
    }

}

