namespace Vibra.DTOs.Song
{
    public class SongCreateDto
    {
        public string Title { get; set; }
        public int DurationSeconds { get; set; }
        public string AudioUrl { get; set; }
        public int TrackNumber { get; set; }
        public Guid AlbumId { get; set; }
    }
}
