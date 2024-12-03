using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.OrganizationIntegrationEvent
{
    public class OfficeAddedIntegrationEvent : OfficeReadModel, IIntegrationEvent
    {
        public string OrganizationId;

        public string GetRoutingKey()
            => "OrganizationUpdated";
    }
}