using MediatR;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Exceptions.Organization;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    internal sealed class AddOrganizationOfficeUsersHandler : IRequestHandler<AddOrganizationOfficeUsers>
    {
        private readonly IOrganizationRepository _repository;
        private readonly IOrganizationReadService _readService;

        public AddOrganizationOfficeUsersHandler(IOrganizationRepository repository, IOrganizationReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(AddOrganizationOfficeUsers request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.OrganizationId))
                throw new OrganizationDoesNotExistsException(request.OrganizationId);

            var org = await _repository.GetByIdAsync(request.OrganizationId);
            var office = org.GetOfficeById(request.OfficeId);

            org.AddRangeOfficeUsers(request.Users, office);
            await _repository.UpdateAsync(org);
        }
    }
}