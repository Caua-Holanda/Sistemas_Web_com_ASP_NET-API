using DomainModel;
using System;
using Vibra.DomainModel.Enums;

namespace Vibra.DomainModel.Entities
{
    public class Subscription : EntityBase
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Guid UserId { get; set; }
        public Guid PlanId { get; set; }
        public virtual User User { get; set; }
        public virtual Plan Plan { get; set; }
    }
}
