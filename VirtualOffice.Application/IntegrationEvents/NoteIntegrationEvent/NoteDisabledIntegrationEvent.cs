using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.NoteIntegrationEvent
{
    public class NoteDisabledIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }

        public string GetRoutingKey()
            => "NoteDisabled";
    }
}