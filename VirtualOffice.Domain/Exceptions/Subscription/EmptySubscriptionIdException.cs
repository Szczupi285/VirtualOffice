using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Subscription
{
    public class EmptySubscriptionIdException : VirtualOfficeException
    {
        public EmptySubscriptionIdException() : base("Subscription id cannot be empty")
        {
        }
    }
}
