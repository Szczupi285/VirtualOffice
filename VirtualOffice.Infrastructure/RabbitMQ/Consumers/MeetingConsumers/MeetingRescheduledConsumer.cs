using MassTransit;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.MeetingConsumers
{
    public class MeetingRescheduledConsumer : IConsumer<MeetingScheduleUpdatedIntegrationEvent>
    {
        private readonly MeetingsService _meetingsService;

        public MeetingRescheduledConsumer(MeetingsService meetingsService)
        {
            _meetingsService = meetingsService;
        }

        public async Task Consume(ConsumeContext<MeetingScheduleUpdatedIntegrationEvent> context)
        {
            await _meetingsService.UpdateScheduleAsync(context.Message.Id, context.Message.StartDate, context.Message.EndDate);
        }
    }
}