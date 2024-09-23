using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.NoteCommands;
using VirtualOffice.Application.Exceptions.Note;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.NoteHandlers
{
    public class DeleteNoteHandler : IRequestHandler<DeleteNote>
    {
        public INoteRepository _repository;
        public INoteReadService _readService;

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