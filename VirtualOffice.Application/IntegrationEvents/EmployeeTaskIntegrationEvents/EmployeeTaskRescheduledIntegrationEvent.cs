using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents
{
    public class EmployeeTaskRescheduledIntegrationEvent : IIntegrationEvent
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string GetRoutingKey()
            => "EmployeeTaskUpdated";
    }
}