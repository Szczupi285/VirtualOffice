using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents
{
    public class EmployeeTaskPriorityUpdatedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public EmployeeTaskPriorityEnum Priority { get; set; }

        public string GetRoutingKey()
            => "UpdateEmployeeTask";
    }
}