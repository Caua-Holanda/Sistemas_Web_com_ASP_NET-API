using Vibra.DTOs.Song;

namespace Vibra.DTOs.Album
{
    public class AlbumResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string CoverUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid BandId { get; set; }
        public ICollection<SongResponseDto> Songs { get; set; }
    }
}
