using Vibra.DomainModel.Enums;

namespace Vibra.DTOs.Account
{
    public class AccountResponseDto
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public decimal Limit { get; set; }
        public AccountStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
    }
}
