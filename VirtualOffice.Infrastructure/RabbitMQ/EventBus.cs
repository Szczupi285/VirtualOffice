using MassTransit;
using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Infrastructure.RabbitMQ
{
    public sealed class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
            where T : class
            => _publishEndpoint.Publish(message, x =>
            {
                x.SetRoutingKey("CalendarEventCreated");
            }, cancellationToken);
    }
}