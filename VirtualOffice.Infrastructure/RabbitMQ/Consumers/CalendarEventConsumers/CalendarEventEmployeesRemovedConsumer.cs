using MassTransit;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.CalendarEventConsumers
{
    public class CalendarEventEmployeesRemovedConsumer : IConsumer<CalendarEventEmployeesRemovedIntegrationEvent>
    {
        private readonly CalendarEventsService _calendarEventsService;

        public CalendarEventEmployeesRemovedConsumer(CalendarEventsService calendarEventsService)
        {
            _calendarEventsService = calendarEventsService;
        }

        public async Task Consume(ConsumeContext<CalendarEventEmployeesRemovedIntegrationEvent> context)
        {
            await _calendarEventsService.RemoveAssignedEmployeesAsync(context.Message.Id, context.Message.AssignedEmployees);
        }
    }
}