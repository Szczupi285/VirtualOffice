using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record UpdateCalendarEventTitle(Guid Id, string Title) : IRequest;
}