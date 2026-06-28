using System;

namespace Vibra.DomainModel.Entities
{
    public class FavoriteBand
    {
        public Guid UserId { get; set; }
        public Guid BandId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
        public virtual Band Band { get; set; }
    }
}
