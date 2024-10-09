using MediatR;
using VirtualOffice.Application.Strategies.ScheduleItemDescriptionStrategies;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.DomainEventHandlers
{
    public class ScheduleItemDescriptionSettedDomainEventHandler : INotificationHandler<ScheduleItemDescriptionSetted>
    {
        private readonly ScheduleItemDescriptionSettedStrategyFactory _factory;

        public ScheduleItemDescriptionSettedDomainEventHandler(ScheduleItemDescriptionSettedStrategyFactory factory)
        {
            _factory = factory;
        }

        public Task Handle(ScheduleItemDescriptionSetted notification, CancellationToken cancellationToken)
        {
            // Since we have many classes inheriting from ScheduleItem
            // We use strategy to determine which base class has been updated and handle the changes accordingly
            var strategy = _factory.GetStrategy(notification.RaisingEntityType);
            return strategy.Handle(notification, cancellationToken);
        }
    }
}