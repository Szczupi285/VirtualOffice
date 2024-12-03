using MediatR;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    internal sealed class CreateOrganizationHandler : IRequestHandler<CreateOrganization>
    {
        private readonly IOrganizationRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public CreateOrganizationHandler(IOrganizationRepository repository, IMediator mediator, IUserRepository userRepository)
        {
            _repository = repository;
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task Handle(CreateOrganization request, CancellationToken cancellationToken)
        {
            var (OrganizationName, UserId) = request;
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);

            Organization org = new(Guid.NewGuid(), OrganizationName, new HashSet<Office>(), new HashSet<ApplicationUser> { user },
                Subscription.CreateDefaultSubscription());
            await _repository.AddAsync(org);

            foreach (var domainEvent in org.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            org.ClearEvents();
        }
    }
}