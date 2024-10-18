using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents
{
    public class MeetingTitleUpdatedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string GetRoutingKey()
            => "MeetingUpdated";
    }
}