using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents
{
    public class EmployeeTaskCreatedIntegrationEvent : EmployeeTaskReadModel, IIntegrationEvent
    {
        public string GetRoutingKey()
            => "EmployeeTaskCreated";
    }
}