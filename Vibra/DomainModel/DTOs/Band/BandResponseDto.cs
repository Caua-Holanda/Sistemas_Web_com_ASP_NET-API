using Vibra.DTOs.Album;

namespace Vibra.DTOs.Band
{
    public class BandResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int? FoundedYear { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<AlbumResponseDto> Albums { get; set; }
    }
}
