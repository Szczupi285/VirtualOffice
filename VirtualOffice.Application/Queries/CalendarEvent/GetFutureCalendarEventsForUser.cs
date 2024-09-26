using MediatR;
using VirtualOffice.Application.DTO.CalendarEvent;

namespace VirtualOffice.Application.Queries.CalendarEvent
{
    public record GetFutureCalendarEventsForUser(Guid UserId) : IRequest<IEnumerable<CalendarEventTitleDTO>>;
}
