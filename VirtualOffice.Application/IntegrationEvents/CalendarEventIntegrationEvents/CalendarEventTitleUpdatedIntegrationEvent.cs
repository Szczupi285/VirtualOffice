using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents
{
    public class CalendarEventTitleUpdatedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string GetRoutingKey()
            => "CalendarEventUpdated";
    }
}