using VirtualOffice.Domain.Exceptions.Subscription;

namespace VirtualOffice.Domain.ValueObjects.Subscription
{
    public sealed record SubscriptionFee
    {
        public decimal Value { get; }

        public SubscriptionFee(decimal subscriptionFee)
        {
            if (subscriptionFee < 0 || subscriptionFee > 25000)
                throw new SubscriptionFeeEitherTooLowOrToHigh(subscriptionFee);

            Value = subscriptionFee;
        }
        public static implicit operator decimal(SubscriptionFee subscriptionFee)
            => subscriptionFee.Value;

        public static implicit operator SubscriptionFee(decimal subscriptionFee)
            => new(subscriptionFee);
    }
}