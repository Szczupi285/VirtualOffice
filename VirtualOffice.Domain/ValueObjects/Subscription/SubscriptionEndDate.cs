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
            if (value < DateTime.Now.AddDays(31))
                throw new SubscriptionEndDateInvalidException(value);

            Value = value;
        }

        public static implicit operator DateTime(SubscriptionEndDate endDate)
            => endDate.Value;

        public static implicit operator SubscriptionEndDate(DateTime endDate)
            => new(endDate);

    }
}


