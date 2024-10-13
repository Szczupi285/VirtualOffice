using MediatR;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.Queries.CalendarEvent
{
    public record GetCalendarEventById(Guid CalendarEventId) : IRequest<CalendarEventReadModel>;
}