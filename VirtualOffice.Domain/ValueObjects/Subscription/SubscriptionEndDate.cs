using VirtualOffice.Domain.Exceptions.Subscription;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.ValueObjects.Subscription
{
    public sealed record SubscriptionEndDate : IComparable<SubscriptionEndDate>
    {
        public DateTime Value { get; }

        public SubscriptionEndDate(DateTime value)
        {
            // since assigning value to SubscriptionEndDate is not fully instant we decrease minutes
            // so it won't return exception if we try assing datetime.UtcNow + 31 days
            if (value < DateTime.UtcNow.AddDays(30).AddMinutes(-1))
                throw new SubscriptionEndDateInvalidException(value);

            Value = value;
        }

        private SubscriptionEndDate(DateTime value, bool validate)
        {
            if (!validate)
                Value = value;
            else
            {
                if (value < DateTime.UtcNow.AddMinutes(-1))
                    throw new ScheduleItemStartDateCannotBePastException(value);

                Value = value;
            }
        }
        public static SubscriptionEndDate CreateWithoutValidation(DateTime value)
        {
            return new SubscriptionEndDate(value, false);
        }

        public static implicit operator DateTime(SubscriptionEndDate endDate)
            => endDate.Value;

        public static implicit operator SubscriptionEndDate(DateTime endDate)
            => new(endDate);

        public int CompareTo(SubscriptionEndDate? other)
        {
            if (this is null || other is null)
                throw new ArgumentNullException();

            if (this.Value > other.Value) return 1;
            if (this.Value < other.Value) return -1;
            return 0;
        }
        public static bool operator <(SubscriptionEndDate left, SubscriptionEndDate right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(SubscriptionEndDate left, SubscriptionEndDate right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator <=(SubscriptionEndDate left, SubscriptionEndDate right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(SubscriptionEndDate left, SubscriptionEndDate right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}