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
        public IOrganizationReadService _OrgReadService;
        public IOfficeReadService _OfficeReadService;

        public AddOrganizationOfficeUsersHandler(IOrganizationRepository repository, IOrganizationReadService orgReadService, IOfficeReadService officeReadService)
        {
            _repository = repository;
            _OrgReadService = orgReadService;
            _OfficeReadService = officeReadService;
        }

        public async Task Handle(AddOrganizationOfficeUsers request, CancellationToken cancellationToken)
        {
            if (!await _OrgReadService.ExistsByIdAsync(request.OrganizationId))
                throw new OrganizationDoesNotExistsException(request.OrganizationId);
            if (!await _OfficeReadService.ExistsByIdAsync(request.OfficeId))
                throw new OfficeDoesNotExistException(request.OfficeId);


            var org = await _repository.GetById(request.OrganizationId);
            var office = org.GetOfficeById(request.OfficeId);


            org.AddRangeOfficeUsers(request.Users, office);
            await _repository.Update(org);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
