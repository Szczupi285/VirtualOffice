using AutoMapper;
using MediatR;
using VirtualOffice.Application.IntegrationEvents.EmployeeTaskIntegrationEvents;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.DomainEvents.EmployeeTask;

namespace VirtualOffice.Application.DomainEventHandlers.EmployeeTaskDomainEventHandlers
{
    internal sealed class EmployeeTaskCreatedDomainEventHandler : INotificationHandler<EmployeeTaskCreated>
    {
        private readonly IMapper _mapper;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public EmployeeTaskCreatedDomainEventHandler(IMapper mapper, IOutboxMessageRepository outboxMessageRepository)
        {
            _mapper = mapper;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(EmployeeTaskCreated notification, CancellationToken cancellationToken)
        {
            EmployeeTaskCreatedIntegrationEvent integrationEvent = new EmployeeTaskCreatedIntegrationEvent
            {
                Id = notification.Id.ToString(),
                Title = notification.Title,
                Description = notification.Description,
                AssignedEmployees = _mapper.Map<List<EmployeeReadModel>>(notification.AssignedEmployees),
                Priority = notification.Priority,
                Status = notification.Status,
                StartDate = notification.StartDate,
                EndDate = notification.EndDate,
            };
            await _outboxMessageRepository.AddOutboxMessageAsync(integrationEvent);
        }
    }
}