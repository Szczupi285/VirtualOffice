using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record RescheduleCalendarEvent(Guid Id, DateTime StartDate, DateTime EndDate) : IRequest;
}