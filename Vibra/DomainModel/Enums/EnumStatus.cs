using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vibra.DomainModel.Enums
{
    public enum AccountStatus
    {
        Active,
        Blocked
    }

    public enum SubscriptionStatus
    {
        Active,
        Canceled,
        Expired
    }

    public enum TransactionStatus
    {
        Pending,
        Authorized,
        Denied
    }
}
