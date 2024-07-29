﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.CalendarEvent;

namespace VirtualOffice.Application.Queries.CalendarEvent
{
    public record GetCalendarEventsForUserByDate(Guid UserId, DateTime StartDate, DateTime EndDate) : IRequest<IEnumerable<CalendarEventTitleDTO>>;
}
