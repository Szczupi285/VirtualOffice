using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.SubscriptionService
{
    public class SubscriptionDatesOverlapException : VirtualOfficeException
    {
        DateTime StartDate;
        DateTime EndDate;
        public SubscriptionDatesOverlapException(DateTime startDate, DateTime endDate) : base($"You cannot add new subscription during this period: {startDate} - {endDate}. It conflicts with another subscription ")
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
