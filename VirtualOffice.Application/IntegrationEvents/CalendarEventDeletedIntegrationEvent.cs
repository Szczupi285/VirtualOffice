using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents
{
    public class CalendarEventDeletedIntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }

        public string GetRoutingKey()
            => "CalendarEventDeleted";
    }
}