using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Balance)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(a => a.Limit)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            builder.Property(a => a.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(a => a.DeletedAt)
                .HasColumnType("datetime2");

            builder.Property(a => a.UserId)
                .IsRequired();

            builder.HasOne(a => a.User)
                .WithOne(u => u.Account)
                .HasForeignKey<Account>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Accounts");
        }
    }
}