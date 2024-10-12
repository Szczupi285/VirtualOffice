namespace VirtualOffice.Application.Interfaces
{
    public interface IOutboxMessageRepository
    {
        public Task AddOutboxMessageAsync(IIntegrationEvent IntegrationEvent, CancellationToken cancellationToken = default);
    }
}