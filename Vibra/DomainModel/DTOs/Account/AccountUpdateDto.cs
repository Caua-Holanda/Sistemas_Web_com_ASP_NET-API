using Vibra.DomainModel.Enums;

namespace Vibra.DTOs.Account
{
    public class AccountUpdateDto
    {
        public decimal Balance { get; set; }
        public decimal Limit { get; set; }
        public AccountStatus Status { get; set; }
    }
}
