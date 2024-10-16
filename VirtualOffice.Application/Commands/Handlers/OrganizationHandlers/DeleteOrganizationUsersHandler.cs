using MediatR;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Exceptions.Organization;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    internal sealed class DeleteOrganizationUsersHandler : IRequest<DeleteOrganizationUsers>
    {
        private readonly IOrganizationRepository _repository;
        private readonly IOrganizationReadService _readService;

        public DeleteOrganizationUsersHandler(IOrganizationRepository repository, IOrganizationReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeleteOrganizationUsers request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new OrganizationDoesNotExistsException(request.Id);

            var org = await _repository.GetByIdAsync(request.Id);

            org.RemoveRangeUsers(request.Users);
            await _repository.UpdateAsync(org);
        }
    }
}