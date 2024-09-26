using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Repositories
{
    public class CalendarEventNotFoundException : VirtualOfficeException
    {
        public CalendarEventNotFoundException(Guid guid) : base($"Calendar Event with Id: {guid} has not been found")
        {
        }
    }
}