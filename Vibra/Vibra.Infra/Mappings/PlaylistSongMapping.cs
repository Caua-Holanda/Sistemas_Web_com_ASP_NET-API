using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vibra.DomainModel.Entities;

public class PlaylistSongMapping : IEntityTypeConfiguration<PlaylistSong>
{
    public void Configure(EntityTypeBuilder<PlaylistSong> builder)
    {
        builder.Property(ps => ps.PlaylistId);
        builder.Property(ps => ps.SongId);

        builder.HasKey(ps => new { ps.PlaylistId, ps.SongId });

        builder.Property(ps => ps.Order)
            .IsRequired()
            .HasColumnType("int");

        builder.HasOne(ps => ps.Playlist)
            .WithMany(p => p.PlaylistSongs)
            .HasForeignKey(e => e.PlaylistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ps => ps.Song)
            .WithMany(s => s.PlaylistSongs)
            .HasForeignKey(e => e.SongId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("PlaylistSongs");
    }
}