using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Exceptions.Organization;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    public class UpdateOrganizationNameHandler : IRequestHandler<UpdateOrganizationName>
    {
        public IOrganizationRepository _repository;
        public IOrganizationReadService _readService;

        public UpdateOrganizationNameHandler(IOrganizationRepository repository, IOrganizationReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdateOrganizationName request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.OrganizationId))
                throw new OrganizationDoesNotExistsException(request.OrganizationId);

            var org = await _repository.GetById(request.OrganizationId);

            org.SetName(request.Name);

            await _repository.UpdateAsync(org);
        }
    }
}