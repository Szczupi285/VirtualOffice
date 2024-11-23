using MediatR;
using VirtualOffice.Application.Commands.NoteCommands;
using VirtualOffice.Application.Exceptions.Note;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.NoteHandlers
{
    internal sealed class DeleteNoteHandler : IRequestHandler<DeleteNote>
    {
        private readonly INoteRepository _repository;
        private readonly INoteReadService _readService;
        private readonly IMediator _mediator;

        public DeleteNoteHandler(INoteRepository repository, INoteReadService noteReadService, IMediator mediator)
        {
            _repository = repository;
            _readService = noteReadService;
            _mediator = mediator;
        }

        public async Task Handle(DeleteNote request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new NoteDoesNoteExistsException(request.Id);

            var entity = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(entity);
            entity.Disable();

            foreach (var domainEvent in entity.Events)
                await _mediator.Publish(domainEvent, cancellationToken);
            entity.ClearEvents();
        }
    }
}