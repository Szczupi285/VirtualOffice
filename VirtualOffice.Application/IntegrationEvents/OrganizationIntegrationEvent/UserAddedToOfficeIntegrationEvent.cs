using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.OrganizationIntegrationEvent
{
    public class UserAddedToOfficeIntegrationEvent : IIntegrationEvent
    {
        public string OrganizationID { get; set; }
        public string OfficeId { get; set; }
        public EmployeeReadModel Employee { get; set; }

        public string GetRoutingKey()
            => "OrganizationUpdated";
    }
}