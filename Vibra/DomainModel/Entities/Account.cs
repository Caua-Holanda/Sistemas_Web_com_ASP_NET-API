using DomainModel;
using System;
using Vibra.DomainModel.Enums;

namespace Vibra.DomainModel.Entities
{
    public class Account : EntityBase
    {
        public decimal Balance { get; set; }
        public decimal Limit { get; set; }
        public AccountStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
