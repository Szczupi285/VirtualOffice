using MediatR;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    internal sealed class CreateOrganizationHandler : IRequestHandler<CreateOrganization>
    {
        private readonly IOrganizationRepository _repository;

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