using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class TooLongScheduleItemTitleException : VirtualOfficeException
    {
        private string Value;

        public TooLongScheduleItemTitleException(string value, uint length) : base($"ScheduleItem Title: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}