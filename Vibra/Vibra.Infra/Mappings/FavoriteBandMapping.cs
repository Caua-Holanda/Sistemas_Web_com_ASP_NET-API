using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class FavoriteBandMapping : IEntityTypeConfiguration<FavoriteBand>
    {
        public void Configure(EntityTypeBuilder<FavoriteBand> builder)
        {
            builder.Property(e => e.UserId);
            builder.Property(e => e.BandId);

            builder.HasKey(fb => new { fb.UserId, fb.BandId });

            builder.Property(fb => fb.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasOne(fb => fb.User)
                .WithMany(u => u.FavoriteBands)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fb => fb.Band)
                .WithMany(b => b.FavoriteBands)
                .HasForeignKey(e => e.BandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("FavoriteBands");
        }
    }
}