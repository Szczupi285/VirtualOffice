using MassTransit;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.CalendarEventConsumers
{
    public class CalendarEventDescriptionUpdatedConsumer : IConsumer<CalendarEventDescriptionUpdatedIntegrationEvent>
    {
        private readonly CalendarEventsService _calendarEventsService;

        public CalendarEventDescriptionUpdatedConsumer(CalendarEventsService calendarEventsService)
        {
            _calendarEventsService = calendarEventsService;
        }

        public async Task Consume(ConsumeContext<CalendarEventDescriptionUpdatedIntegrationEvent> context)
        {
            await _calendarEventsService.UpdateDescriptionAsync(context.Message.Id, context.Message.Description);
        }
    }
}