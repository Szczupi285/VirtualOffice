using VirtualOffice.Domain.DomainEvents;

namespace VirtualOffice.Application.Interfaces
{
    public interface IOutboxMessageRepository
    {
        public Task AddOutboxMessageAsync(IDomainEvent domainEvent);
    }
}