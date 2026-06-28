namespace Vibra.DTOs.Plan
{
    public class PlanResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal MonthlyPrice { get; set; }
        public string Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
