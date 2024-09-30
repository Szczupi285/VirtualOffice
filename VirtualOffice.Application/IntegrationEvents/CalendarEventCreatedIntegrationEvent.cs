using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.Events
{
    public class CalendarEventCreatedIntegrationEvent : CalendarEventReadModel, IEvent
    {
        public string GetRoutingKey()
            => "CalendarEventCreated";
    }
}