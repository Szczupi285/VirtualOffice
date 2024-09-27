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

        public Task PublisAsync<IEvent>(IEvent message, CancellationToken cancellationToken = default)
            where IEvent : class
            => _publishEndpoint.Publish<IEvent>(message, cancellationToken);
    }
}