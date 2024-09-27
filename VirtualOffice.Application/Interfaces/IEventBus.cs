namespace VirtualOffice.Application.Interfaces
{
    public interface IEventBus
    {
        // IEvent must be a class because of publish<T> method of
        // IPublishEndpoint requires a reference type
        Task PublishAsync<T>(T message,
            CancellationToken cancellationToken = default) where T : class;
    }
}