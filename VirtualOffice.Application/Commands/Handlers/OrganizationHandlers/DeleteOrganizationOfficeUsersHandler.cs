using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Exceptions.Office;
using VirtualOffice.Application.Exceptions.Organization;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    public class DeleteOrganizationOfficeUsersHandler : IRequestHandler<DeleteOrganizationOfficeUsers>
    {
        public IOrganizationRepository _repository;
        public IOrganizationReadService _OrgReadService;

        public DeleteOrganizationOfficeUsersHandler(IOrganizationRepository repository, IOrganizationReadService orgReadService)
        {
            _repository = repository;
            _OrgReadService = orgReadService;
        }

        public async Task Handle(DeleteOrganizationOfficeUsers request, CancellationToken cancellationToken)
        {
            if (!await _OrgReadService.ExistsByIdAsync(request.OrganizationId))
                throw new OrganizationDoesNotExistsException(request.OrganizationId);

            var org = await _repository.GetByIdAsync(request.OrganizationId);
            var office = org.GetOfficeById(request.OfficeId);

            org.RemoveRangeOfficeUsers(request.Users, office);
            await _repository.UpdateAsync(org);
        }
    }
}