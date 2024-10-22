using MediatR;
using VirtualOffice.Application.Commands.NoteCommands;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.NoteHandlers
{
    internal sealed class CreateNoteHandler : IRequestHandler<CreateNote>
    {
        private readonly INoteRepository _repository;
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;

        public CreateNoteHandler(INoteRepository repository, IMediator mediator,
            IUserRepository userRepository, IUserReadService userReadService)
        {
            _repository = repository;
            _mediator = mediator;
            _userRepository = userRepository;
            _userReadService = userReadService;
        }

        public async Task Handle(CreateNote request, CancellationToken cancellationToken)
        {
            var (Title, Content, UserId) = request;
            if (!await _userReadService.ExistsByIdAsync(UserId, cancellationToken))
                throw new EmployeeNotFoundException(UserId);
            var user = await _userRepository.GetByIdAsync(UserId);

            Note note = new(Guid.NewGuid(), Title, Content, user);

            await _repository.AddAsync(note);

            foreach (var domainEvent in note.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            note.ClearEvents();
        }
    }
}