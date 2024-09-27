using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.Events
{
    public class CalendarEventCreatedEvent : CalendarEventReadModel, IEvent
    {
        public string GetRoutingKey()
            => "CalendarEventCreated";
    }
}