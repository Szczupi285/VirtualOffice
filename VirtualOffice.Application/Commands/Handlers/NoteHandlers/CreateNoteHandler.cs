using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.NoteCommands;
using VirtualOffice.Application.Exceptions.Note;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.NoteHandlers
{
    public class CreateNoteHandler : IRequestHandler<CreateNote>
    {

        public INoteRepository _repository;

        public CreateNoteHandler(INoteRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateNote request, CancellationToken cancellationToken)
        {
            var(Title, Content, User) = request;

            Note note = new(Guid.NewGuid(), Title, Content, User); 

            await _repository.Add(note);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
