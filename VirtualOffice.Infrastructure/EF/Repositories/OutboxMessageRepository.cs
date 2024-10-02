using Newtonsoft.Json;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Outbox;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly WriteDbContext _dbContext;

        public OutboxMessageRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOutboxMessageAsync(IEvent domainEvent)
        {
            DateTime dateTime = DateTime.UtcNow;
            await _dbContext.OutboxMessages.AddAsync(
                new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccuredOnUtc = dateTime,
                    Type = domainEvent.GetType().Name,
                    Content = JsonConvert.SerializeObject(
                        domainEvent,
                        // Store type of the object that was serialized
                        // that will allow us to deserialize json into IDomain object
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                        })
                });
            await _dbContext.SaveChangesAsync();
        }
    }
}