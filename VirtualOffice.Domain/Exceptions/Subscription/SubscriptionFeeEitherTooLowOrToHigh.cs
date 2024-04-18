using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Subscription
{
    public class SubscriptionFeeEitherTooLowOrToHigh : VirtualOfficeException
    {
        decimal Value;
        public SubscriptionFeeEitherTooLowOrToHigh(decimal value) : base($"Value: {value} is either too low or too high for a subscription fee") 
        {
            Value = value;
        }
    }
}
