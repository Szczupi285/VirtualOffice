using MassTransit;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.CalendarEventConsumers
{
    public sealed class CalendarEventEmployeesAddedConsumer : IConsumer<CalendarEventEmployeesAddedIntegrationEvent>
    {
        private readonly CalendarEventsService _calendarEventsService;

        public CalendarEventEmployeesAddedConsumer(CalendarEventsService calendarEventsService)
        {
            _calendarEventsService = calendarEventsService;
        }

        public async Task Consume(ConsumeContext<CalendarEventEmployeesAddedIntegrationEvent> context)
        {
            await _calendarEventsService.UpdateAsync(context.Message.Id, context.Message);
        }
    }
}