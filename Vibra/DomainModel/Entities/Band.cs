using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vibra.DomainModel.Entities
{
    public class Band : EntityBase
    {
        public Band()
        {
            Albums = new HashSet<Album>();
            FavoriteBands = new HashSet<FavoriteBand>();
        }

        public string Name { get; set; }
        public string Genre { get; set; }
        public int? FoundedYear { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<FavoriteBand> FavoriteBands { get; set; }
    }
}
