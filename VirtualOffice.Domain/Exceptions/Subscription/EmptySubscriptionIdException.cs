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