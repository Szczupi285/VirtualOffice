using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class InvalidScheduleItemEndDateException : VirtualOfficeException
    {
        private DateTime Value;

        public InvalidScheduleItemEndDateException(DateTime value) : base($"ScheduleItem EndDate cannot be in the past")
        {
            Value = value;
        }
    }
}