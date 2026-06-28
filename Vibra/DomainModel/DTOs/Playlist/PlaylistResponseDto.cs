using Vibra.DTOs.PlaylistSong;

namespace Vibra.DTOs.Playlist
{
    public class PlaylistResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public ICollection<PlaylistSongResponseDto> PlaylistSongs { get; set; }
    }
}
