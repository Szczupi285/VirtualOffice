using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.OrganizationIntegrationEvent
{
    public class OrganizationCreatedIntegrationEvent : OrganizationReadModel, IIntegrationEvent
    {
        public string GetRoutingKey()
            => "OrganizationCreated";
    }
}