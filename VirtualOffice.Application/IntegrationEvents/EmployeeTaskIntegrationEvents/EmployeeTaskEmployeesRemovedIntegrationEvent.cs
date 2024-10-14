using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents
{
    public class EmployeeTaskEmployeesRemovedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public List<EmployeeReadModel> AssignedEmployees { get; set; }

        public string GetRoutingKey()
            => "EmployeeTaskUpdated";
    }
}