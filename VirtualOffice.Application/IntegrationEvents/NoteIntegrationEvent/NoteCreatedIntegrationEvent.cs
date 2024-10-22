using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.NoteCreatedIntegrationEvent
{
    public class NoteCreatedIntegrationEvent : NoteReadModel, IIntegrationEvent
    {
        public string GetRoutingKey()
            => "NoteCreated";
    }
}