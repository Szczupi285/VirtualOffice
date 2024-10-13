using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents
{
    public class CalendarEventTitleUpdatedIntegrationEvent : CalendarEventReadModel, IIntegrationEvent
    {
        public string GetRoutingKey()
            => "CalendarEventUpdated";
    }
}