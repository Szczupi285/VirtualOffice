using MediatR;
using VirtualOffice.Application.Strategies.ScheduleItemRescheduledStrategies;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.DomainEventHandlers.ScheduleItemDomainEventHandlers
{
    public class ScheduleItemRescheduledDomainEventHandler : INotificationHandler<ScheduleItemRescheduled>
    {
        private readonly ScheduleItemRescheduledStrategyFactory _factory;

        public ScheduleItemRescheduledDomainEventHandler(ScheduleItemRescheduledStrategyFactory factory)
        {
            _factory = factory;
        }

        public Task Handle(ScheduleItemRescheduled notification, CancellationToken cancellationToken)
        {
            // Since we have many classes inheriting from ScheduleItem
            // We use strategy to determine which base class has been updated and handle the changes accordingly
            var strategy = _factory.GetStrategy(notification.RaisingEntityType);
            return strategy.Handle(notification, cancellationToken);
        }
    }
}