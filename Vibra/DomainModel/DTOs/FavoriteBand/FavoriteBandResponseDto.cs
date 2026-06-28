using Vibra.DTOs.Band;

namespace Vibra.DTOs.FavoriteBand
{

    public class FavoriteBandResponseDto
    {
        public Guid UserId { get; set; }
        public Guid BandId { get; set; }
        public DateTime CreatedAt { get; set; }
        public BandResponseDto Band { get; set; }
    }
}
