using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Subscription
{
    public class SubscriptionEndDateInvalidException : VirtualOfficeException
    {
        private DateTime _endDate;

        public SubscriptionEndDateInvalidException(DateTime endDate) : base($"SubscriptionEndDate: '{endDate}' cannot be earlier than 31 days from now.")
        {
            _endDate = endDate;
        }
    }
}