using DomainModel;
using System;

namespace Vibra.DomainModel.Entities
{
    public class Card : EntityBase
    {
        public string CardholderName { get; set; }
        public string TokenizedNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Brand { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
