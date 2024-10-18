using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents
{
    public class MeetingScheduleUpdatedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string GetRoutingKey()
            => "MeetingUpdated";
    }
}