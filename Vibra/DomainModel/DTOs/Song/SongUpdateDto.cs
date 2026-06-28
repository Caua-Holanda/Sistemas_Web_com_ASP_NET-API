namespace Vibra.DTOs.Song
{
    public class SongUpdateDto
    {
        public string Title { get; set; }
        public int DurationSeconds { get; set; }
        public string AudioUrl { get; set; }
        public int TrackNumber { get; set; }
    }
}
