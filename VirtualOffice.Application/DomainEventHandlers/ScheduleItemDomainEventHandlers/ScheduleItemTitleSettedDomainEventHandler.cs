using MediatR;
using VirtualOffice.Application.Strategies.ScheduleItemTitleStrategies;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.DomainEventHandlers
{
    internal sealed class ScheduleItemTitleSettedDomainEventHandler : INotificationHandler<ScheduleItemTitleSetted>
    {
        private readonly ScheduleItemTitleSettedStrategyFactory _factory;

        public ScheduleItemTitleSettedDomainEventHandler(ScheduleItemTitleSettedStrategyFactory factory)
        {
            _factory = factory;
        }

        public Task Handle(ScheduleItemTitleSetted notification, CancellationToken cancellationToken)
        {
            // Since we have many classes inheriting from ScheduleItem
            // We use strategy to determine which base class has been updated and handle the changes accordingly
            var strategy = _factory.GetStrategy(notification.RaisingEntityType);
            return strategy.Handle(notification, cancellationToken);
        }
    }
}