namespace VirtualOffice.Application.Interfaces
{
    public interface IOutboxMessageRepository
    {
        public Task AddOutboxMessageAsync(IEvent IntegrationEvent);
    }
}