using Vibra.DomainModel.Enums;

namespace Vibra.DTOs.Account
{
    public class AccountCreateDto
    {
        public decimal Balance { get; set; }
        public decimal Limit { get; set; }
        public AccountStatus Status { get; set; }
    }
}
