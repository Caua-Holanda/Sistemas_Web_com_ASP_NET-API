using Vibra.DTOs.Account;
using Vibra.DTOs.Card;
using Vibra.DTOs.FavoriteBand;
using Vibra.DTOs.FavoriteSong;
using Vibra.DTOs.Playlist;
using Vibra.DTOs.Subscription;
using Vibra.DTOs.Transaction;

namespace Vibra.DTOs.User
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public AccountResponseDto Account { get; set; }
        public ICollection<CardResponseDto> Cards { get; set; }
        public ICollection<SubscriptionResponseDto> Subscriptions { get; set; }
        public ICollection<PlaylistResponseDto> Playlists { get; set; }
        public ICollection<FavoriteSongResponseDto> FavoriteSongs { get; set; }
        public ICollection<FavoriteBandResponseDto> FavoriteBands { get; set; }
        public ICollection<TransactionResponseDto> Transactions { get; set; }
    }
}
