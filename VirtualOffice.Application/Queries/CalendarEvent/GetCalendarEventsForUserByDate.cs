using MediatR;
using VirtualOffice.Application.DTO.CalendarEvent;

namespace VirtualOffice.Application.Queries.CalendarEvent
{
    public record GetCalendarEventsForUserByDate(Guid UserId, DateTime StartDate, DateTime EndDate) : IRequest<IEnumerable<CalendarEventTitleDescDTO>>;
}