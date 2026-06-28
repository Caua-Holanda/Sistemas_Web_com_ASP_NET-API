namespace Vibra.DTOs.PlaylistSong
{
    public class PlaylistSongCreateDto
    {
        public Guid PlaylistId { get; set; }
        public Guid SongId { get; set; }
        public int Order { get; set; }
    }
}
