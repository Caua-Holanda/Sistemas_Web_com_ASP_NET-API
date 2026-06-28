using Vibra.DomainModel.Enums;

namespace Vibra.DTOs.Subscription
{
    public class SubscriptionCreateDto
    {
        public Guid UserId { get; set; }
        public Guid PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
