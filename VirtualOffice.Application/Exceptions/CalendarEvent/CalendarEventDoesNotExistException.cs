using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.CalendarEvent
{
    public class CalendarEventDoesNotExistException : VirtualOfficeException
    {
        public CalendarEventDoesNotExistException(Guid guid) : base($"Calendar event with id: {guid} doesn't exists")
        {
        }
    }
}
