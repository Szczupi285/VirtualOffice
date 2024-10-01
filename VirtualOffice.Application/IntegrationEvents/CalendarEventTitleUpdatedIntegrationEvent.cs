using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents
{
    public class CalendarEventTitleUpdatedIntegrationEvent : CalendarEventReadModel, IEvent
    {
        public string GetRoutingKey()
            => "CalendarEventUpdated";
    }
}