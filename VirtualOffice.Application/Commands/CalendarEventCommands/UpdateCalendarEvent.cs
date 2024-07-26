using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record UpdateCalendarEvent(Guid Guid, string Title, string Description, DateTime StartDate, DateTime EndDate) : IRequest;

}
