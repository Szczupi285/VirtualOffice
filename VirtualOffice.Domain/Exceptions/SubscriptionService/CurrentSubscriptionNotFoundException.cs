using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.SubscriptionService
{
    public class CurrentSubscriptionNotFoundException : VirtualOfficeException
    {
        public CurrentSubscriptionNotFoundException() : base("You do not have any active subscription")
        {
        }
    }
}
