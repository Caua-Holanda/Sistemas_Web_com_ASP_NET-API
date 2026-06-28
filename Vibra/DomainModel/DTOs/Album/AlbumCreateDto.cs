namespace Vibra.DTOs.Album
{
    public class AlbumCreateDto
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string CoverUrl { get; set; }
        public Guid BandId { get; set; }
    }
}
