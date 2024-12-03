using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents.OrganizationIntegrationEvent;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.OrganizationEvents;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Application.DomainEventHandlers.OrganizationDomainEventHandlers
{
    internal class OrganizationCreatedDomainEventHandler : INotificationHandler<OrganizationCreated>
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IMapper _mapper;

        public OrganizationCreatedDomainEventHandler(IOutboxMessageRepository outboxMessageRepository, IMapper mapper)
        {
            _outboxMessageRepository = outboxMessageRepository;
            _mapper = mapper;
        }

        public async Task Handle(OrganizationCreated notification, CancellationToken cancellationToken)
        {
            OrganizationUserLimit userLimit;

            if (notification.subscription._subType == Domain.Consts.SubscriptionTypeEnum.Unlimited)
                userLimit = null!;
            else
                userLimit = (ushort)notification.subscription._subType;

            OrganizationCreatedIntegrationEvent integrationEvent = new()
            {
                Id = notification.id.Value.ToString(),
                Name = notification.name.Value,
                // while creating organization office list in empty
                Offices = new List<OfficeReadModel>(),
                Employees = _mapper.Map<List<EmployeeReadModel>>(notification.organizationUsers),
                Subscription = new SubscriptionReadModel()
                {
                    Id = notification.subscription.Id.Value.ToString(),
                    StartDate = notification.subscription._subStartDate,
                    EndDate = notification.subscription._subEndDate,
                    Fee = notification.subscription._subscriptionFee,
                    IsPayed = notification.subscription._isPayed,
                },
                UserLimit = userLimit,
            };

            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}