using MassTransit;
using VirtualOffice.Application.Events;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers
{
    public class CalendarEventDescriptionUpdatedConsumer : IConsumer<CalendarEventDescriptionUpdated>
    {
        private readonly CalendarEventsService _calendarEventsService;

        public CalendarEventDescriptionUpdatedConsumer(CalendarEventsService calendarEventsService)
        {
            _calendarEventsService = calendarEventsService;
        }

        public async Task Consume(ConsumeContext<CalendarEventDescriptionUpdated> context)
        {
            await _calendarEventsService.UpdateAsync(context.Message.Id, context.Message);
        }
    }
}