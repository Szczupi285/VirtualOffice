using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.CalendarEvent
{
    public class CalendarEventAlreadyExistsException : VirtualOfficeException
    {
        public CalendarEventAlreadyExistsException(Guid guid) : base($"Calendar event with id: {guid} already exists")
        {
        }
    }
}
