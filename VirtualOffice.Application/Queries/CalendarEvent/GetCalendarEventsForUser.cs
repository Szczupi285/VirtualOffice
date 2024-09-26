using MediatR;
using VirtualOffice.Application.DTO.CalendarEvent;

namespace VirtualOffice.Application.Queries.CalendarEvent
{
    public record GetCalendarEventsForUser(Guid UserId) : IRequest<IEnumerable<CalendarEventTitleDTO>>;
}
