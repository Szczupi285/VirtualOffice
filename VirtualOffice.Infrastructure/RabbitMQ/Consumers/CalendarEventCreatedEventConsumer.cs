using MassTransit;
using VirtualOffice.Application.Events;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers
{
    public sealed class CalendarEventCreatedEventConsumer : IConsumer<CalendarEventCreatedEvent>
    {
        private readonly CalendarEventsService _calendarEventsService;

        public CalendarEventCreatedEventConsumer(CalendarEventsService calendarEventsService)
        {
            _calendarEventsService = calendarEventsService;
        }

        public async Task Consume(ConsumeContext<CalendarEventCreatedEvent> context)
        {
            await _calendarEventsService.CreateAsync(context.Message);
        }
    }
}