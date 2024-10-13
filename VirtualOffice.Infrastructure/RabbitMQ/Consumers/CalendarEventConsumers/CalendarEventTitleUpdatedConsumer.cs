using MassTransit;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.CalendarEventConsumers
{
    public class CalendarEventTitleUpdatedConsumer : IConsumer<CalendarEventTitleUpdatedIntegrationEvent>
    {
        private readonly CalendarEventsService _calendarEventsService;

        public CalendarEventTitleUpdatedConsumer(CalendarEventsService calendarEventsService)
        {
            _calendarEventsService = calendarEventsService;
        }

        public async Task Consume(ConsumeContext<CalendarEventTitleUpdatedIntegrationEvent> context)
        {
            await _calendarEventsService.UpdateTitleAsync(context.Message.Id, context.Message.Title);
        }
    }
}