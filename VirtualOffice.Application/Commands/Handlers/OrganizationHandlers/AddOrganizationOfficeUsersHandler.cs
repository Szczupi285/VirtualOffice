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
    public class AddOrganizationOfficeUsersHandler : IRequestHandler<AddOrganizationOfficeUsers>
    {
        public IOrganizationRepository _repository;
        public IOrganizationReadService _readService;

        public AddOrganizationOfficeUsersHandler(IOrganizationRepository repository, IOrganizationReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(AddOrganizationOfficeUsers request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.OrganizationId))
                throw new OrganizationDoesNotExistsException(request.OrganizationId);

            var org = await _repository.GetById(request.OrganizationId);
            var office = org.GetOfficeById(request.OfficeId);

            org.AddRangeOfficeUsers(request.Users, office);
            await _repository.UpdateAsync(org);
        }
    }
}