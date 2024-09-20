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
            var (Title, Content, UserId) = request;

            Note note = new(Guid.NewGuid(), Title, Content, UserId);

            await _repository.AddAsync(note);
        }
    }
}