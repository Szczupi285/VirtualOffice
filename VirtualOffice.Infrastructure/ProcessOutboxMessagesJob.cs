using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Outbox;
using VirtualOffice.Infrastructure.EF;

namespace VirtualOffice.Infrastructure
{
    public class ProcessOutboxMessagesJob : IJob
    {
        private readonly WriteDbContext _dbContext;
        private readonly IEventBus _eventBus;

        public ProcessOutboxMessagesJob(WriteDbContext dbContext, IEventBus eventBus)
        {
            _dbContext = dbContext;
            _eventBus = eventBus;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _dbContext.
                Set<OutboxMessage>()
                .Where(m => m.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync(context.CancellationToken);
            foreach (OutboxMessage msg in messages)
            {
                try
                {
                    // handle debugging
                    IIntegrationEvent? IntegrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(msg.Content, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                    // Messages are not consumed if integration event isn't explicitly casted to a type inheriting from IEvent
                    // Temporary solution
                    dynamic dynamicEvent = IntegrationEvent;

                    msg.ProcessedOnUtc = DateTime.UtcNow;
                    await _eventBus.PublishAsync(dynamicEvent);
                }
                catch (Exception ex)
                {
                    // todo: logger
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}