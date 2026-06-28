using DomainModel;
using System;
using Vibra.DomainModel.Enums;

namespace Vibra.DomainModel.Entities
{
    public class Transaction : EntityBase
    {
        public string Merchant { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public TransactionStatus Status { get; set; }
        public string DenialReason { get; set; } = string.Empty;
        public DateTime? LastAuthorization { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Guid UserId { get; set; }
        public Guid? CardId { get; set; }
        public virtual User User { get; set; }
        public virtual Card Card { get; set; }
    }
}
