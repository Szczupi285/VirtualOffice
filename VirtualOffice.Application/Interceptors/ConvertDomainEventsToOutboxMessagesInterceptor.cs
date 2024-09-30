using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using VirtualOffice.Application.Outbox;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Application.Interceptors
{
    public sealed class ConvertDomainEventsToOutboxMessagesInterceptor
        : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync
            (DbContextEventData eventData, InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if (dbContext is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var outboxMessages = dbContext.ChangeTracker.Entries<AggregateRoot<ScheduleItemId>>()
                .Select(x => x.Entity)
                .SelectMany(x =>
                {
                    var domainEvents = x.Events.ToList();
                    x.ClearEvents();
                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccuredOnUtc = DateTime.UtcNow,
                    Type = domainEvent.GetType().Name,
                    Content = JsonConvert.SerializeObject(
                        domainEvent,
                        // Store type of the object that was serialized
                        // that will allow us to deserialize json into IDomain object
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                        })
                })
                .ToList();

            dbContext.Set<OutboxMessage>().AddRange(outboxMessages);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}