using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Subscription
{
    public class SubscriptionStartDateCannotBePast : VirtualOfficeException
    {
        DateTime _startDate;
        public SubscriptionStartDateCannotBePast(DateTime startDate) : base($"SubscriptionStartDate: '{startDate}' cannot be in the past.")
        {
            _startDate = startDate;
        }
    }
}
