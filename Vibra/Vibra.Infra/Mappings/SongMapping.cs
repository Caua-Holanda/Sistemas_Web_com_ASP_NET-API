using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class SongMapping : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

            builder.HasIndex(s => s.Title)
                .HasDatabaseName("IX_Song_Title");

            builder.Property(s => s.DurationSeconds)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(s => s.TrackNumber)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(s => s.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(s => s.DeletedAt)
                .HasColumnType("datetime2");

            builder.Property(s => s.AudioUrl)
                .HasMaxLength(500)
                .HasColumnType("varchar(500)");

            builder.Property(s => s.AlbumId);

            builder.HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(e => e.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Songs");
        }
    }
}