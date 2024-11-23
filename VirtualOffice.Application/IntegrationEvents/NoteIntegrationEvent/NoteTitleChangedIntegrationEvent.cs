using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.NoteIntegrationEvent
{
    public class NoteTitleChangedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string GetRoutingKey()
            => "NoteUpdated";
    }
}