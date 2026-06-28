using Microsoft.EntityFrameworkCore;
using Vibra.DomainModel.Entities;
using Vibra.DomainModel.Enums;

namespace Vibra.Infra.Data
{
    public class VibraDbContext : DbContext
    {
        public VibraDbContext(DbContextOptions<VibraDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        public DbSet<FavoriteSong> FavoriteSongs { get; set; }
        public DbSet<FavoriteBand> FavoriteBands { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Name).IsRequired().HasMaxLength(200);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(u => u.CreatedAt).IsRequired();
                entity.Property(u => u.UpdatedAt);
                entity.Property(u => u.DeletedAt);
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(a => a.Balance).HasPrecision(18, 2);
                entity.Property(a => a.Limit).HasPrecision(18, 2);
                entity.Property(a => a.Status).HasConversion<string>().HasMaxLength(20);
                entity.Property(a => a.CreatedAt).IsRequired();
                entity.Property(a => a.UpdatedAt);
                entity.Property(a => a.DeletedAt);

                entity.HasOne(a => a.User)
                    .WithOne(u => u.Account)
                    .HasForeignKey<Account>(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(c => c.CardholderName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.TokenizedNumber).IsRequired().HasMaxLength(255);
                entity.Property(c => c.ExpiryDate).IsRequired().HasMaxLength(7);
                entity.Property(c => c.Brand).IsRequired().HasMaxLength(50);
                entity.Property(c => c.UpdatedAt);
                entity.Property(c => c.DeletedAt);

                entity.HasOne(c => c.User)
                    .WithMany(u => u.Cards)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.MonthlyPrice).HasPrecision(18, 2);
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.UpdatedAt);
                entity.Property(p => p.DeletedAt);
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(s => s.StartDate).IsRequired();
                entity.Property(s => s.Status).HasConversion<string>().HasMaxLength(20);
                entity.Property(s => s.UpdatedAt);
                entity.Property(s => s.DeletedAt);

                entity.HasOne(s => s.User)
                    .WithMany(u => u.Subscriptions)
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.Plan)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(s => s.PlanId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Band>(entity =>
            {
                entity.Property(b => b.Name).IsRequired().HasMaxLength(200);
                entity.HasIndex(b => b.Name);
                entity.Property(b => b.Genre).HasMaxLength(100);
                entity.Property(b => b.ImageUrl).HasMaxLength(500);
                entity.Property(b => b.UpdatedAt);
                entity.Property(b => b.DeletedAt);
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(a => a.Title).IsRequired().HasMaxLength(200);
                entity.HasIndex(a => a.Title);
                entity.Property(a => a.CoverUrl).HasMaxLength(500);
                entity.Property(a => a.UpdatedAt);
                entity.Property(a => a.DeletedAt);

                entity.HasOne(a => a.Band)
                    .WithMany(b => b.Albums)
                    .HasForeignKey(a => a.BandId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(s => s.Title).IsRequired().HasMaxLength(200);
                entity.HasIndex(s => s.Title);
                entity.Property(s => s.AudioUrl).HasMaxLength(500);
                entity.Property(s => s.DurationSeconds).IsRequired();
                entity.Property(s => s.TrackNumber).IsRequired();
                entity.Property(s => s.UpdatedAt);
                entity.Property(s => s.DeletedAt);

                entity.HasOne(s => s.Album)
                    .WithMany(a => a.Songs)
                    .HasForeignKey(s => s.AlbumId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.CreatedAt).IsRequired();
                entity.Property(p => p.UpdatedAt);
                entity.Property(p => p.DeletedAt);

                entity.HasOne(p => p.User)
                    .WithMany(u => u.Playlists)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PlaylistSong>(entity =>
            {
                entity.HasKey(ps => new { ps.PlaylistId, ps.SongId });

                entity.Property(ps => ps.Order).IsRequired();

                entity.HasOne(ps => ps.Playlist)
                    .WithMany(p => p.PlaylistSongs)
                    .HasForeignKey(ps => ps.PlaylistId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ps => ps.Song)
                    .WithMany(s => s.PlaylistSongs)
                    .HasForeignKey(ps => ps.SongId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FavoriteSong>(entity =>
            {
                entity.HasKey(fs => new { fs.UserId, fs.SongId });

                entity.Property(fs => fs.CreatedAt).IsRequired();

                entity.HasOne(fs => fs.User)
                    .WithMany(u => u.FavoriteSongs)
                    .HasForeignKey(fs => fs.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(fs => fs.Song)
                    .WithMany(s => s.FavoriteSongs)
                    .HasForeignKey(fs => fs.SongId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FavoriteBand>(entity =>
            {
                entity.HasKey(fb => new { fb.UserId, fb.BandId });

                entity.Property(fb => fb.CreatedAt).IsRequired();

                entity.HasOne(fb => fb.User)
                    .WithMany(u => u.FavoriteBands)
                    .HasForeignKey(fb => fb.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(fb => fb.Band)
                    .WithMany(b => b.FavoriteBands)
                    .HasForeignKey(fb => fb.BandId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(t => t.Merchant).IsRequired().HasMaxLength(200);
                entity.Property(t => t.Amount).HasPrecision(18, 2);
                entity.Property(t => t.Timestamp).IsRequired();
                entity.Property(t => t.Status).HasConversion<string>().HasMaxLength(20);
                entity.Property(t => t.DenialReason).HasMaxLength(255);
                entity.HasIndex(t => t.Merchant);
                entity.HasIndex(t => t.Timestamp);
                entity.Property(t => t.LastAuthorization);
                entity.Property(t => t.UpdatedAt);
                entity.Property(t => t.DeletedAt);

                entity.HasOne(t => t.User)
                    .WithMany(u => u.Transactions)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.Card)
                    .WithMany()
                    .HasForeignKey(t => t.CardId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}