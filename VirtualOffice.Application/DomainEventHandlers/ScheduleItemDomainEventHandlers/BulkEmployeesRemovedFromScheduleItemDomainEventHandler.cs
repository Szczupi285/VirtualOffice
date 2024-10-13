using MediatR;
using VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Remove;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.DomainEventHandlers.ScheduleItemDomainEventHandlers
{
    public class BulkEmployeesRemovedFromScheduleItemDomainEventHandler : INotificationHandler<BulkEmployeesRemovedFromScheduleItem>
    {
        private readonly ScheduleItemEmployeesRemovedStrategyFactory _factory;

        public BulkEmployeesRemovedFromScheduleItemDomainEventHandler(ScheduleItemEmployeesRemovedStrategyFactory factory)
        {
            _factory = factory;
        }

        public Task Handle(BulkEmployeesRemovedFromScheduleItem notification, CancellationToken cancellationToken)
        {
            // Since we have many classes inheriting from ScheduleItem
            // We use strategy to determine which base class has been updated and handle the changes accordingly
            var strategy = _factory.GetStrategy(notification.RaisingEntityType);
            return strategy.Handle(notification, cancellationToken);
        }
    }
}