
using Vibra.DomainModel.Enums;

namespace Vibra.DTOs.Transaction
{
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }
        public string Merchant { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public TransactionStatus Status { get; set; }
        public string DenialReason { get; set; }
        public DateTime? LastAuthorization { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid? CardId { get; set; }
    }
}
