using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Subscription;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.Subscription
{
    public sealed record SubscriptionStartDate
    {
        public DateTime Value { get; }

        public SubscriptionStartDate(DateTime value)
        {
            // since assigning value to SubscriptionStartDate is not fully instant we decrease minutes 
            // so it won't return exception if we try assing datetime.Now
            if (value < DateTime.Now.AddMinutes(-1))
                throw new SubscriptionStartDateCannotBePastException(value);

            Value = value;  
        }

        public static implicit operator DateTime(SubscriptionStartDate startDate)
            => startDate.Value;

        public static implicit operator SubscriptionStartDate(DateTime startDate)
            => new(startDate);
    }
}
