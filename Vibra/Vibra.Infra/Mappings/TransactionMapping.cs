using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Merchant)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

            builder.HasIndex(t => t.Merchant);

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(t => t.Timestamp)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(t => t.Timestamp);

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            builder.Property(t => t.DenialReason)
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

            builder.Property(t => t.LastAuthorization)
                .HasColumnType("datetime2");

            builder.Property(t => t.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(t => t.DeletedAt)
                .HasColumnType("datetime2");

            builder.Property(e => e.UserId);
            builder.Property(t => t.CardId);

            builder.HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Card)
                .WithMany()
                .HasForeignKey(e => e.CardId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("Transactions");
        }
    }
}