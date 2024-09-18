using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.SubscriptionService
{
    public class SubscriptionNotFoundException : VirtualOfficeException
    {
        private Guid Value;

        public SubscriptionNotFoundException(Guid value) : base($"Subscription with Id: {value} has not been found")
        {
            Value = value;
        }
    }
}