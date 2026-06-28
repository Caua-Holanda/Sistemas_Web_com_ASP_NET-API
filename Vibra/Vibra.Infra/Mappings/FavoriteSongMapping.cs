using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class FavoriteSongMapping : IEntityTypeConfiguration<FavoriteSong>
    {
        public void Configure(EntityTypeBuilder<FavoriteSong> builder)
        {
            builder.Property(e => e.UserId);
            builder.Property(ps => ps.SongId);

            builder.HasKey(fs => new { fs.UserId, fs.SongId });

            builder.Property(fs => fs.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasOne(fs => fs.User)
                .WithMany(u => u.FavoriteSongs)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fs => fs.Song)
                .WithMany(s => s.FavoriteSongs)
                .HasForeignKey(e => e.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("FavoriteSongs");
        }
    }
}