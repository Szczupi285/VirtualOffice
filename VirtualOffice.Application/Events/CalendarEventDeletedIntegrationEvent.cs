using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.Events
{
    public class CalendarEventDeletedIntegrationEvent : IEvent
    {
        public Guid Id { get; set; }

        public string GetRoutingKey()
            => "CalendarEventDeleted";
    }
}