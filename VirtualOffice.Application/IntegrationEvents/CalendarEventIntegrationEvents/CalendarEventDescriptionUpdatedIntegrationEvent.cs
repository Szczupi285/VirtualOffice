using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents
{
    public class CalendarEventDescriptionUpdatedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public string GetRoutingKey()
            => "CalendarEventUpdated";
    }
}