using MediatR;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Exceptions.Organization;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    internal sealed class AddOfficeHandler : IRequestHandler<AddOffice>
    {
        private readonly IOrganizationRepository _repository;
        private readonly IOrganizationReadService _readService;
        private readonly IMediator _mediator;

        public AddOfficeHandler(IOrganizationRepository repository, IOrganizationReadService readService
            , IMediator mediator)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
        }

        public async Task Handle(AddOffice request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.OrganizationId))
                throw new OrganizationDoesNotExistsException(request.OrganizationId);

            var org = await _repository.GetByIdAsync(request.OrganizationId);

            Office office = new(Guid.NewGuid(), request.Name, request.Description, request.Members);
            org.AddOffice(office);

            await _repository.UpdateAsync(org);

            foreach (var domainEvent in org.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            org.ClearEvents();
        }
    }
}