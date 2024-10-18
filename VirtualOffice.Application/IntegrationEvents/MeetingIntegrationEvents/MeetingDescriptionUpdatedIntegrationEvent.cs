using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents
{
    public class MeetingDescriptionUpdatedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public string GetRoutingKey()
            => "MeetingUpdated";
    }
}