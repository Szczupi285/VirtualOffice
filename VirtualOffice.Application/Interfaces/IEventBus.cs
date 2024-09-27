namespace VirtualOffice.Application.Interfaces
{
    public interface IEventBus
    {
        // T must be a class because of publish<T> method of
        // IPublishEndpoint requires a reference type
        // also it needs to be IEvent so we can use GetRoutingKey in implementation
        Task PublishAsync<T>(T message,
            CancellationToken cancellationToken = default) where T : class, IEvent;
    }
}