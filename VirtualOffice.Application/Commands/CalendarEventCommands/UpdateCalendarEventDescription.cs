using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record UpdateCalendarEventDescription(Guid Id, string Description) : IRequest;
}