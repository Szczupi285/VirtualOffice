using MediatR;
using VirtualOffice.Application.Strategies.ScheduleItemEmployeesStrategies.Add;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;

namespace VirtualOffice.Application.DomainEventHandlers.ScheduleItemDomainEventHandlers
{
    public class BulkEmployeesAddedToScheduleItemDomainEventHandler : INotificationHandler<BulkEmployeesAddedToScheduleItem>
    {
        private readonly ScheduleItemEmployeesAddedStrategyFactory _factory;

        public BulkEmployeesAddedToScheduleItemDomainEventHandler(ScheduleItemEmployeesAddedStrategyFactory factory)
        {
            _factory = factory;
        }

        public Task Handle(BulkEmployeesAddedToScheduleItem notification, CancellationToken cancellationToken)
        {
            // Since we have many classes inheriting from ScheduleItem
            // We use strategy to determine which base class has been updated and handle the changes accordingly
            var strategy = _factory.GetStrategy(notification.RaisingEntityType);
            return strategy.Handle(notification, cancellationToken);
        }
    }
}