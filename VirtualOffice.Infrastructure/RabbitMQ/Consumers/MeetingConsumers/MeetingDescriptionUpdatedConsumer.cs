using MassTransit;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.MeetingConsumers
{
    public class MeetingDescriptionUpdatedConsumer : IConsumer<MeetingDescriptionUpdatedIntegrationEvent>
    {
        private readonly MeetingsService _meetingsService;

        public MeetingDescriptionUpdatedConsumer(MeetingsService meetingsService)
        {
            _meetingsService = meetingsService;
        }

        public async Task Consume(ConsumeContext<MeetingDescriptionUpdatedIntegrationEvent> context)
        {
            await _meetingsService.UpdateDescriptionAsync(context.Message.Id, context.Message.Description);
        }
    }
}