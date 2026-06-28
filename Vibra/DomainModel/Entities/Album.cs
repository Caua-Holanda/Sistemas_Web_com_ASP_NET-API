using DomainModel;
using System;
using System.Collections.Generic;

namespace Vibra.DomainModel.Entities
{
    public class Album : EntityBase
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        public string Title { get; set; }
        public int Year { get; set; }
        public string CoverUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Guid BandId { get; set; }
        public virtual Band Band { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
