using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Subscription;

namespace VirtualOffice.Domain.ValueObjects.Subscription
{
    public sealed record SubscriptionEndDate
    {
        public DateTime Value { get; }

        public SubscriptionEndDate(DateTime value)
        {
            // since assigning value to SubscriptionEndDate is not fully instant we decrease minutes 
            // so it won't return exception if we try assing datetime.Now + 31 days 
            if (value < DateTime.Now.AddDays(31).AddMinutes(-1))
                throw new SubscriptionEndDateInvalidException(value);

            Value = value;
        }

        public static implicit operator DateTime(SubscriptionEndDate endDate)
            => endDate.Value;

        public static implicit operator SubscriptionEndDate(DateTime endDate)
            => new(endDate);

    }
}


