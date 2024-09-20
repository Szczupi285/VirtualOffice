using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    public class CreateOrganizationHandler : IRequestHandler<CreateOrganization>
    {
        public IOrganizationRepository _repository;

        public CreateOrganizationHandler(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateOrganization request, CancellationToken cancellationToken)
        {
            var (OrganizationName, Subscription, User) = request;

            Organization org = new(Guid.NewGuid(), OrganizationName, new HashSet<Office>(), new HashSet<ApplicationUser> { User }, Subscription);
            await _repository.AddAsync(org);
        }
    }
}