using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vibra.DomainModel.Entities
{
    public class Plan : EntityBase
    {
        public Plan()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public string Name { get; set; }
        public decimal MonthlyPrice { get; set; }
        public string Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
