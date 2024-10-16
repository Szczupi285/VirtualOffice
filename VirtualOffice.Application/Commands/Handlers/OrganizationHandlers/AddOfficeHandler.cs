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

        public AddOfficeHandler(IOrganizationRepository repository, IOrganizationReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(AddOffice request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.OrganizationId))
                throw new OrganizationDoesNotExistsException(request.OrganizationId);

            var org = await _repository.GetByIdAsync(request.OrganizationId);

            Office office = new(Guid.NewGuid(), request.Name, request.Description, request.Members);
            org.AddOffice(office);

            await _repository.UpdateAsync(org);
        }
    }
}