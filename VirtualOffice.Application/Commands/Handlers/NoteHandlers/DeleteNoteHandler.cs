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

        public DeleteNoteHandler(INoteRepository repository, INoteReadService noteReadService)
        {
            _repository = repository;
            _readService = noteReadService;
        }

        public async Task Handle(DeleteNote request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new NoteDoesNoteExistsException(request.Id);

            var entity = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(entity);
        }
    }
}