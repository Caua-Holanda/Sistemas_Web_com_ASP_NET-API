namespace Vibra.DTOs.Song
{
    public class SongResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int DurationSeconds { get; set; }
        public string AudioUrl { get; set; }
        public int TrackNumber { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid AlbumId { get; set; }
    }
}
