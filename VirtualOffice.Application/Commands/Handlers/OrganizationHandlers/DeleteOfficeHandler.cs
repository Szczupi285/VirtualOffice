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
    public class DeleteOfficeHandler : IRequest<DeleteOffice>
    {
        public IOrganizationRepository _repository;
        public IOrganizationReadService _readService;

        public DeleteOfficeHandler(IOrganizationRepository repository, IOrganizationReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeleteOffice request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.OrganizationId))
                throw new OrganizationDoesNotExistsException(request.OrganizationId);

            var org = await _repository.GetById(request.OrganizationId);

            Office office = org.GetOfficeById(request.OfficeId);
            org.RemoveOffice(office);

            await _repository.UpdateAsync(org);
        }
    }
}