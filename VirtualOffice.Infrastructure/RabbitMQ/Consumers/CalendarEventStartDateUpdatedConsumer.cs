using MassTransit;
using VirtualOffice.Application.IntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers
{
    public sealed class CalendarEventStartDateUpdatedConsumer : IConsumer<CalendarEventStartDateUpdatedIntegrationEvent>
    {
        private readonly CalendarEventsService _calendarEventsService;

        public CalendarEventStartDateUpdatedConsumer(CalendarEventsService calendarEventsService)
        {
            _calendarEventsService = calendarEventsService;
        }

        public async Task Consume(ConsumeContext<CalendarEventStartDateUpdatedIntegrationEvent> context)
        {
            await _calendarEventsService.UpdateAsync(context.Message.Id, context.Message);
        }
    }
}