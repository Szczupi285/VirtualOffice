using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents
{
    public class EmployeeTaskDeletedIntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }

        public string GetRoutingKey()
            => "EmployeeTaskDeleted";
    }
}