using MediatR;
using VirtualOffice.Application.Commands.OrganizationCommands;
using VirtualOffice.Application.Exceptions.Organization;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.OrganizationHandlers
{
    internal sealed class AddOfficeHandler : IRequestHandler<AddOffice>
    {
        private readonly IOrganizationRepository _repository;
        private readonly IOrganizationReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public AddOfficeHandler(IOrganizationRepository repository, IOrganizationReadService readService
            , IMediator mediator, IUserRepository userRepository)
        {
            _repository = repository;
            _readService = readService;
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task Handle(AddOffice request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.OrganizationId, cancellationToken))
                throw new OrganizationDoesNotExistsException(request.OrganizationId);

            var org = await _repository.GetByIdAsync(request.OrganizationId);
            HashSet<ApplicationUser> users = new HashSet<ApplicationUser>();

            foreach (var guid in request.Members)
            {
                users.Add(await _userRepository.GetByIdAsync(guid));
            }

            Office office = new(Guid.NewGuid(), request.Name, request.Description, users);
            org.AddOffice(office);

            await _repository.UpdateAsync(org);

            foreach (var domainEvent in org.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            org.ClearEvents();
        }
    }
}