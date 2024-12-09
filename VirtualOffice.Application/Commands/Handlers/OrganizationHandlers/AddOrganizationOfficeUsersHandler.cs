using MediatR;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Exceptions.Organization;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    internal sealed class AddOrganizationOfficeUsersHandler : IRequestHandler<AddOrganizationOfficeUsers>
    {
        private readonly IOrganizationRepository _repository;
        private readonly IOrganizationReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public AddOrganizationOfficeUsersHandler(IOrganizationRepository repository, IOrganizationReadService readService,
           IUserRepository userRepository, IMediator mediator)
        {
            _repository = repository;
            _readService = readService;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task Handle(AddOrganizationOfficeUsers request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.OrganizationId))
                throw new OrganizationDoesNotExistsException(request.OrganizationId);

            var org = await _repository.GetByIdAsync(request.OrganizationId);
            var office = org.GetOfficeById(request.OfficeId);
            List<ApplicationUser> users = new List<ApplicationUser>();

            foreach (var id in request.UserIds)
            {
                users.Add(await _userRepository.GetByIdAsync(id, cancellationToken));
            }

            org.AddRangeOfficeUsers(users, office);
            await _repository.UpdateAsync(org);

            foreach (var domainEvent in org.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            org.ClearEvents();
        }
    }
}