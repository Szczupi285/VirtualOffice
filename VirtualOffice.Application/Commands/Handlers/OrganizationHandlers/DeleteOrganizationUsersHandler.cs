using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Exceptions.Organization;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    public class DeleteOrganizationUsersHandler : IRequest<DeleteOrganizationUsers>
    {
        public IOrganizationRepository _repository;
        public IOrganizationReadService _readService;

        public DeleteOrganizationUsersHandler(IOrganizationRepository repository, IOrganizationReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeleteOrganizationUsers request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new OrganizationDoesNotExistsException(request.Id);

            var org = await _repository.GetById(request.Id);

            org.RemoveRangeUsers(request.Users);
            await _repository.UpdateAsync(org);
        }
    }
}