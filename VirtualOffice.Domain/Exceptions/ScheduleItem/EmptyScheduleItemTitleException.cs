using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VIrtualOffice.Domain.Exceptions.ScheduleItem
{
    public class EmptyScheduleItemTitleException : VirtualOfficeException
    {
        public EmptyScheduleItemTitleException() : base("ScheduleItem Title cannot be empty")
        {
        }
    }
}