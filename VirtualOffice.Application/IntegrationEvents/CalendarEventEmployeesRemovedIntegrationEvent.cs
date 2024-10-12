using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents
{
    public class CalendarEventEmployeesRemovedIntegrationEvent : CalendarEventReadModel, IIntegrationEvent
    {
        public string GetRoutingKey()
            => "CalendarEventUpdated";
    }
}