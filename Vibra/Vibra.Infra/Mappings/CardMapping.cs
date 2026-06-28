using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class CardMapping : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CardholderName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.TokenizedNumber)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

            builder.Property(c => c.ExpiryDate)
                .IsRequired()
                .HasMaxLength(7)
                .HasColumnType("varchar(7)");

            builder.Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(c => c.DeletedAt)
                .HasColumnType("datetime2");

            builder.Property(e => e.UserId);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Cards)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Cards");
        }
    }
}