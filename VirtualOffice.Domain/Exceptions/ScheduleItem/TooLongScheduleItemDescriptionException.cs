using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class TooLongScheduleItemDescriptionException : VirtualOfficeException
    {
        private string Value;

        public TooLongScheduleItemDescriptionException(string value, uint length) : base($"ScheduleItem Description: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}