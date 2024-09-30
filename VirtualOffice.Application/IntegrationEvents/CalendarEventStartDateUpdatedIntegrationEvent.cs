using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.IntegrationEvents
{
    public class CalendarEventStartDateUpdatedIntegrationEvent : CalendarEventReadModel, IEvent
    {
        public string GetRoutingKey()
            => "CalendarEventUpdated";
    }
}