using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Subscription;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.Subscription
{
    public sealed record SubscriptionStartDate : IComparable<SubscriptionStartDate>
    {
        public DateTime Value { get; }

        public SubscriptionStartDate(DateTime value)
        {
            // since assigning value to SubscriptionStartDate is not fully instant we decrease minutes 
            // so it won't return exception if we try assing datetime.UtcNow
            if (value < DateTime.UtcNow.AddMinutes(-1))
                throw new SubscriptionStartDateCannotBePastException(value);

            Value = value;  
        }

        public static implicit operator DateTime(SubscriptionStartDate startDate)
            => startDate.Value;

        public static implicit operator SubscriptionStartDate(DateTime startDate)
            => new(startDate);

        public int CompareTo(SubscriptionStartDate? other)
        {
            if (this is null || other is null)
                throw new ArgumentNullException();

            if(this.Value > other.Value) return 1;
            if(this.Value < other.Value) return -1;
            return 0;
        }
        public static bool operator <(SubscriptionStartDate left, SubscriptionStartDate right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(SubscriptionStartDate left, SubscriptionStartDate right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator <=(SubscriptionStartDate left, SubscriptionStartDate right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(SubscriptionStartDate left, SubscriptionStartDate right)
        {
            return left.CompareTo(right) >= 0;
        }

    }
}
