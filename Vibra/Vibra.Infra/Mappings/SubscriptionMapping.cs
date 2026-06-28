using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class SubscriptionMapping : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.StartDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(s => s.EndDate)
                .HasColumnType("datetime2");

            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            builder.Property(s => s.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(s => s.DeletedAt)
                .HasColumnType("datetime2");

            builder.Property(e => e.UserId);
            builder.Property(s => s.PlanId);

            builder.HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Plan)
                .WithMany(p => p.Subscriptions)
                .HasForeignKey(e => e.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Subscriptions");
        }
    }
}