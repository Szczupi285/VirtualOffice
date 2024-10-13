using MassTransit;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.CalendarEventConsumers
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
            await _calendarEventService.UpdateScheduleAsync(context.Message.Id, context.Message.StartDate, context.Message.EndDate);
        }
    }
}