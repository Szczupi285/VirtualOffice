using MassTransit;
using VirtualOffice.Application.IntegrationEvents.MeetingIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.MeetingConsumers
{
    public class MeetingDeletedConsumer : IConsumer<MeetingDeletedIntegrationEvent>
    {
        private readonly MeetingsService _service;

        public MeetingDeletedConsumer(MeetingsService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<MeetingDeletedIntegrationEvent> context)
        {
            await _service.RemoveAsync(context.Message.Id);
        }
    }
}