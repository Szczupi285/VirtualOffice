using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents
{
    public class EmployeeTaskTitleUpdatedIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string GetRoutingKey()
            => "EmployeeTaskUpdated";
    }
}