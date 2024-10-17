using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents
{
    public class MeetingCreatedIntegrationEvent : MeetingReadModel, IIntegrationEvent
    {
        public string GetRoutingKey()
            => "MeetingCreated";
    }
}