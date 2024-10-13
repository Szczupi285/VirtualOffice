using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents
{
    public class CalendarEventDeletedIntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }

        public string GetRoutingKey()
            => "CalendarEventDeleted";
    }
}