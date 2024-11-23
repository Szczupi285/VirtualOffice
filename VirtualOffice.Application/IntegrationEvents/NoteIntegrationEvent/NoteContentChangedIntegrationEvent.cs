using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.NoteIntegrationEvent
{
    public class NoteContentChangedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string GetRoutingKey()
            => "NoteUpdated";
    }
}
