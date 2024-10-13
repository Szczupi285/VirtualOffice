using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents
{
    public class CalendarEventRescheduledIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string GetRoutingKey()
            => "CalendarEventUpdated";
    }
}