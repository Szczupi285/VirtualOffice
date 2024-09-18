using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Subscription
{
    public class SubscriptionFeeEitherTooLowOrToHigh : VirtualOfficeException
    {
        private decimal Value;

        public SubscriptionFeeEitherTooLowOrToHigh(decimal value) : base($"Value: {value} is either too low or too high for a subscription fee")
        {
            Value = value;
        }
    }
}