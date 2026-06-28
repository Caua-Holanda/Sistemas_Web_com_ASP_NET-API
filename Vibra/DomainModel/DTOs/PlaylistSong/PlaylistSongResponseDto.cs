using Vibra.DTOs.Song;

namespace Vibra.DTOs.PlaylistSong
{
    public class PlaylistSongResponseDto
    {
        public Guid PlaylistId { get; set; }
        public Guid SongId { get; set; }
        public int Order { get; set; }
        public SongResponseDto Song { get; set; }
    }
}
