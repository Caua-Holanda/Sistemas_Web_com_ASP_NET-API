using Vibra.DomainModel.Enums;

namespace Vibra.DTOs.Subscription
{
    public class SubscriptionResponseDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid PlanId { get; set; }
    }
}
