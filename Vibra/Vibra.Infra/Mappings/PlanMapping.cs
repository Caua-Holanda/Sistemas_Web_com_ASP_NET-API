using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class PlanMapping : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(p => p.MonthlyPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(p => p.Description)
                .HasMaxLength(500)
                .HasColumnType("varchar(500)");

            builder.Property(p => p.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(p => p.DeletedAt)
                .HasColumnType("datetime2");

            builder.ToTable("Plans");
        }
    }
}