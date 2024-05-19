using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Event;

namespace VirtualOffice.Domain.Entities
{
    public class CalendarEvent : AbstractEvent
    {
        public CalendarEvent(EventId id, EventTitle titile, EventStartDate startDate, EventEndDate endDate, EventDescription eventDescription, ICollection<ApplicationUser> visibleTo) : base(id, titile, startDate, endDate, eventDescription, visibleTo)
        {
        }
    }
}
