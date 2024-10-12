using MassTransit;
using VirtualOffice.Application.IntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers
{
    public class CalendarEventDeletedConsumer : IConsumer<CalendarEventDeletedIntegrationEvent>
    {
        private readonly CalendarEventsService _calendarEventService;

        public CalendarEventDeletedConsumer(CalendarEventsService calendarEventService)
        {
            _calendarEventService = calendarEventService;
        }

        public async Task Consume(ConsumeContext<CalendarEventDeletedIntegrationEvent> context)
        {
            await _calendarEventService.RemoveAsync(context.Message.Id.ToString());
        }
    }
}