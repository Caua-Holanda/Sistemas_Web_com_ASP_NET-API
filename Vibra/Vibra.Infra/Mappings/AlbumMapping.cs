using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class AlbumMapping : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

            builder.HasIndex(a => a.Title)
                .HasDatabaseName("IX_Album_Title");

            builder.Property(a => a.Year)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(a => a.CoverUrl)
                .HasMaxLength(500)
                .HasColumnType("varchar(500)");

            builder.Property(a => a.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(a => a.DeletedAt)
                .HasColumnType("datetime2");

            builder.Property(e => e.BandId);

            builder.HasOne(a => a.Band)
                .WithMany(b => b.Albums)
                .HasForeignKey(e => e.BandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Albums");
        }
    }
}