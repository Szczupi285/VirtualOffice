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
    public class AddOrganizationUsersHandler : IRequestHandler<AddOrganizationUsers>
    {

        public IOrganizationRepository _repository;
        public IOrganizationReadService _readService;

        public AddOrganizationUsersHandler(IOrganizationRepository repository, IOrganizationReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(AddOrganizationUsers request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new OrganizationDoesNotExistsException(request.Id);

            var org = await _repository.GetById(request.Id);

            org.AddRangeUsers(request.Users);
            await _repository.Update(org);
            await _repository.SaveAsync(cancellationToken);


        }
    }
}
