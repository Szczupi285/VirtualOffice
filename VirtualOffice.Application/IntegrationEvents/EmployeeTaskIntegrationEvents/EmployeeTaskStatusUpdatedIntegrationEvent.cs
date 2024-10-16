using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents
{
    public class EmployeeTaskStatusUpdatedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public EmployeeTaskStatusEnum Status { get; set; }

        public string GetRoutingKey()
            => "EmployeeTaskUpdated";
    }
}