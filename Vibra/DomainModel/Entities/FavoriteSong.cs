using System;

namespace Vibra.DomainModel.Entities
{
    public class FavoriteSong
    {
        public Guid UserId { get; set; }
        public Guid SongId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
        public virtual Song Song { get; set; }
    }
}
