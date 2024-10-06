using MassTransit;
using VirtualOffice.Application.Events;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers
{
    public class CalendarEventTitleUpdatedConsumer : IConsumer<CalendarEventTitleUpdated>
    {
        private readonly CalendarEventsService _calendarEventsService;

        public CalendarEventTitleUpdatedConsumer(CalendarEventsService calendarEventsService)
        {
            _calendarEventsService = calendarEventsService;
        }

        public async Task Consume(ConsumeContext<CalendarEventTitleUpdated> context)
        {
            await _calendarEventsService.UpdateAsync(context.Message.Id, context.Message);
        }
    }
}