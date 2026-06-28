using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

namespace Vibra.Infra.Mappings
{
    public class BandMapping : IEntityTypeConfiguration<Band>
    {
        public void Configure(EntityTypeBuilder<Band> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

            builder.HasIndex(b => b.Name)
                .HasDatabaseName("IX_Band_Name");

            builder.Property(b => b.Genre)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(b => b.FoundedYear)
                .HasColumnType("int");

            builder.Property(b => b.ImageUrl)
                .HasMaxLength(500)
                .HasColumnType("varchar(500)");

            builder.Property(b => b.UpdatedAt)
                .HasColumnType("datetime2");

            builder.Property(b => b.DeletedAt)
                .HasColumnType("datetime2");

            builder.ToTable("Bands");
        }
    }
}