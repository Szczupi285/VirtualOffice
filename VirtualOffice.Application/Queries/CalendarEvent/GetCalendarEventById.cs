using MediatR;
using VirtualOffice.Application.DTO.CalendarEvent;

namespace VirtualOffice.Application.Queries.CalendarEvent
{
    public record GetCalendarEventById(Guid CalendarEventId) : IRequest<CalendarEventDTO>;
}
