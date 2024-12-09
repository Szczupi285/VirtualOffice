using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents.OrganizationIntegrationEvent;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.OrganizationEvents;

namespace VirtualOffice.Application.DomainEventHandlers.OrganizationDomainEventHandlers
{
    public class UserAddedToOfficeDomainEventHandler : INotificationHandler<UserAddedToOffice>
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IMapper _mapper;

        public UserAddedToOfficeDomainEventHandler(IOutboxMessageRepository outboxMessageRepository, IMapper mapper)
        {
            _outboxMessageRepository = outboxMessageRepository;
            _mapper = mapper;
        }

        public async Task Handle(UserAddedToOffice notification, CancellationToken cancellationToken)
        {
            UserAddedToOfficeIntegrationEvent integrationEvent = new()
            {
                OrganizationID = notification.organization.Id.Value.ToString(),
                OfficeId = notification.office.Id.Value.ToString(),
                Employee = _mapper.Map<EmployeeReadModel>(notification.User)
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent, cancellationToken);
        }
    }
}