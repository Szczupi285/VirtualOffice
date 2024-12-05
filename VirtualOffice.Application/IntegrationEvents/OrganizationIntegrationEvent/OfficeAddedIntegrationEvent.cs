using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.OrganizationIntegrationEvent
{
    public class OfficeAddedIntegrationEvent : IIntegrationEvent
    {
        public string OrganizationId { get; set; }

        public OfficeReadModel Office { get; set; }

        public string GetRoutingKey()
            => "OrganizationUpdated";
    }
}