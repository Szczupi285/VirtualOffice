using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.Events
{
    public class CalendarEventTitleUpdated : CalendarEventReadModel, IEvent
    {
        public string GetRoutingKey()
            => "CalendarEventUpdated";
    }
}