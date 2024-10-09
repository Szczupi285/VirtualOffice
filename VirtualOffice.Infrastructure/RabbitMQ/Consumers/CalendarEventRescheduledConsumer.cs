using MassTransit;
using VirtualOffice.Application.Events;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers
{
    public class CalendarEventRescheduledConsumer : IConsumer<CalendarEventRescheduledIntegrationEvent>
    {
        private readonly CalendarEventsService _calendarEventService;

        public CalendarEventRescheduledConsumer(CalendarEventsService calendarEventService)
        {
            _calendarEventService = calendarEventService;
        }

        public async Task Consume(ConsumeContext<CalendarEventRescheduledIntegrationEvent> context)
        {
            await _calendarEventService.UpdateAsync(context.Message.Id, context.Message);
        }
    }
}