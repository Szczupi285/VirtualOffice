using MassTransit;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.MeetingConsumers
{
    public class MeetingCreatedConsumer : IConsumer<MeetingCreatedIntegrationEvent>
    {
        private readonly MeetingsService _service;

        public MeetingCreatedConsumer(MeetingsService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<MeetingCreatedIntegrationEvent> context)
        {
            await _service.CreateAsync(context.Message);
        }
    }
}