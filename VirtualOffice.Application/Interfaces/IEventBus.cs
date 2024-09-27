namespace VirtualOffice.Application.Interfaces
{
    public interface IEventBus
    {
        // IEvent must be a class because of publish<T> method of
        // IPublishEndpoint requires a reference type
        Task PublisAsync<IEvent>(IEvent message,
            CancellationToken cancellationToken = default) where IEvent : class;
    }
}