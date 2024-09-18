using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class EndDateCannotBeBeforeStartDate : VirtualOfficeException
    {
        private DateTime EndDate;
        private DateTime StartDate;

        public EndDateCannotBeBeforeStartDate(DateTime startDate, DateTime endDate) : base($"End Date: {endDate} is before Start Date: {startDate}")
        {
            EndDate = endDate;
            StartDate = startDate;
        }
    }
}