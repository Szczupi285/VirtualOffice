using MassTransit;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.MeetingConsumers
{
    public class MeetingTitleUpdatedConsumer : IConsumer<MeetingTitleUpdatedIntegrationEvent>
    {
        private readonly MeetingsService _service;

        public MeetingTitleUpdatedConsumer(MeetingsService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<MeetingTitleUpdatedIntegrationEvent> context)
        {
            await _service.UpdateTitleAsync(context.Message.Id, context.Message.Title);
        }
    }
}