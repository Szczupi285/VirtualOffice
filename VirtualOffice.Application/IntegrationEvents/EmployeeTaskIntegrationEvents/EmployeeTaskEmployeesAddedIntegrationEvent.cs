using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents
{
    public class EmployeeTaskEmployeesAddedIntegrationEvent : EmployeeTaskReadModel, IIntegrationEvent
    {
        public string GetRoutingKey()
            => "EmployeeTaskUpdated";
    }
}