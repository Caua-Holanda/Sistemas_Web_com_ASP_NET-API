using Vibra.DTOs.Song;

namespace Vibra.DTOs.FavoriteSong
{
    public class FavoriteSongResponseDto
    {
        public Guid UserId { get; set; }
        public Guid SongId { get; set; }
        public DateTime CreatedAt { get; set; }
        public SongResponseDto Song { get; set; }
    }
}
