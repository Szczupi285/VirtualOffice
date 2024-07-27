using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.CalendarEvent
{
    public record GetAllCalendarEventsForUserByDate(Guid UserId, DateTime StartDate, DateTime EndDate) : IRequest<IEnumerable<CalendarEventDTO>>;
}
