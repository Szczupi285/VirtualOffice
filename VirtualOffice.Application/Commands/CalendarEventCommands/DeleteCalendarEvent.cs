using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record DeleteCalendarEvent(Guid Id) : IRequest;
}