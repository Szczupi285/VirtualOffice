using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents
{
    public class CalendarEventRescheduledIntegrationEvent : CalendarEventReadModel, IIntegrationEvent
    {
        public string GetRoutingKey()
            => "CalendarEventUpdated";
    }
}