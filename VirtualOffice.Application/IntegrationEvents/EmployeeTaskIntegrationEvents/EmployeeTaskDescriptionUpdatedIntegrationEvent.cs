using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents
{
    public class EmployeeTaskDescriptionUpdatedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public string GetRoutingKey()
            => "EmployeeTaskUpdated";
    }
}