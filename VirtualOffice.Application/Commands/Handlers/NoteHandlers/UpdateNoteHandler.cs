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
    public class UpdateNoteHandler : IRequestHandler<UpdateNote>
    {
        public INoteRepository _repository;
        public INoteReadService _readService;

        public UpdateNoteHandler(INoteRepository repository, INoteReadService noteReadService)
        {
            _repository = repository;
            _readService = noteReadService;
        }

        public async Task Handle(UpdateNote request, CancellationToken cancellationToken)
        {
            var (Id, Title, Content) = request;

            if (!await _readService.ExistsByIdAsync(Id))
                throw new NoteDoesNoteExistsException(Id);

            var note = await _repository.GetById(Id);

            if (note._title != Title)
                note.EditTitle(Title);
            if (note._content != Content)
                note.EditContent(Content);

            await _repository.UpdateAsync(note);
        }
    }
}