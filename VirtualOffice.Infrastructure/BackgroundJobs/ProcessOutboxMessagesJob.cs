using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using VirtualOffice.Application.Outbox;
using VirtualOffice.Domain.DomainEvents;
using VirtualOffice.Infrastructure.EF;

namespace VirtualOffice.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob : IJob
    {
        private readonly WriteDbContext _dbContext;
        private readonly IPublisher _publisher;

        public ProcessOutboxMessagesJob(WriteDbContext dbContext, IPublisher publisher)
        {
            _dbContext = dbContext;
            _publisher = publisher;
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
                    IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(msg.Content, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                    msg.ProcessedOnUtc = DateTime.UtcNow;
                    await _publisher.Publish(domainEvent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error");
                }

            }
            await _dbContext.SaveChangesAsync();
        }
    }
}