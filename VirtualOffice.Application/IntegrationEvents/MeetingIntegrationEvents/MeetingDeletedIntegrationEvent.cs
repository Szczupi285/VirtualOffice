using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents
{
    public class MeetingDeletedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }

        public string GetRoutingKey()
            => "MeetingDeleted";
    }
}