using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Subscription
{
    public class SubscriptionStartDateCannotBePastException : VirtualOfficeException
    {
        private DateTime _startDate;

        public SubscriptionStartDateCannotBePastException(DateTime startDate) : base($"SubscriptionStartDate: '{startDate}' cannot be in the past.")
        {
            _startDate = startDate;
        }
    }
}