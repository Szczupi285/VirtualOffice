using MassTransit;
using VirtualOffice.Application.IntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers
{
    public class CalendarEventEmployeesRemovedConsumer : IConsumer<CalendarEventEmployeesRemovedIntegrationEvent>
    {
        private readonly CalendarEventsService _calendarEventsService;

        public async Task Consume(ConsumeContext<CalendarEventEmployeesRemovedIntegrationEvent> context)
        {
            await _calendarEventsService.UpdateAsync(context.Message.Id, context.Message);
        }
    }
}