using DomainModel;
using Vibra.DomainModel.Entities;

public class User : EntityBase
{
    public User()
    {
        Cards = new HashSet<Card>();
        Subscriptions = new HashSet<Subscription>();
        Playlists = new HashSet<Playlist>();
        FavoriteSongs = new HashSet<FavoriteSong>();
        FavoriteBands = new HashSet<FavoriteBand>();
        Transactions = new HashSet<Transaction>();
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual Account Account { get; set; }
    public virtual ICollection<Card> Cards { get; set; }
    public virtual ICollection<Subscription> Subscriptions { get; set; }
    public virtual ICollection<Playlist> Playlists { get; set; }
    public virtual ICollection<FavoriteSong> FavoriteSongs { get; set; }
    public virtual ICollection<FavoriteBand> FavoriteBands { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; }
}