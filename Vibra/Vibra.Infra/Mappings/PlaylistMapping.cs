using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class PlaylistMapping : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(p => p.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(p => p.DeletedAt)
                .HasColumnType("datetime2");

            builder.Property(e => e.UserId);

            builder.HasOne(p => p.User)
                .WithMany(u => u.Playlists)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Playlists");
        }
    }
}