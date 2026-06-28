using Vibra.DomainModel.Enums;

namespace Vibra.DTOs.Subscription
{
    public class SubscriptionUpdateDto
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
