using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class ScheduleItemStartDateCannotBePastException : VirtualOfficeException
    {
        private DateTime _startDate;

        public ScheduleItemStartDateCannotBePastException(DateTime startDate) : base($"ScheduleItemStartDate: '{startDate}' cannot be in the past.")
        {
            _startDate = startDate;
        }
    }
}