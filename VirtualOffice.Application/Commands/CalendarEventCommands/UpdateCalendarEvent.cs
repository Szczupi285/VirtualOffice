using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record UpdateCalendarEvent(Guid Id, string Title, string Description, DateTime StartDate, DateTime EndDate) : IRequest;
}