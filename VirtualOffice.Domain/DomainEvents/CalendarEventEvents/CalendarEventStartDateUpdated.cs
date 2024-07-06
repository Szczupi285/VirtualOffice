using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.DomainEvents.CalendarEventEvents
{
    public record CalendarEventStartDateUpdated(CalendarEvent calendarEvent, ScheduleItemStartDate StartDate) : IDomainEvent;

}
