using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents.OrganizationIntegrationEvent;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.OrganizationEvents;

namespace VirtualOffice.Application.DomainEventHandlers.OrganizationDomainEventHandlers
{
    internal class AddOfficeDomainEventHandler : INotificationHandler<OfficeAdded>
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IMapper _mapper;

        public AddOfficeDomainEventHandler(IOutboxMessageRepository outboxMessageRepository, IMapper mapper)
        {
            _outboxMessageRepository = outboxMessageRepository;
            _mapper = mapper;
        }

        public async Task Handle(OfficeAdded notification, CancellationToken cancellationToken)
        {
            OfficeAddedIntegrationEvent integrationEvent = new()
            {
                OrganizationId = notification.organization.Id.Value.ToString(),
                Office = new OfficeReadModel()
                {
                    Id = notification.office.Id.Value.ToString(),
                    Description = notification.office._description,
                    Name = notification.office._officeName,
                    Employees = _mapper.Map<List<EmployeeReadModel>>(notification.office._members),
                }
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}