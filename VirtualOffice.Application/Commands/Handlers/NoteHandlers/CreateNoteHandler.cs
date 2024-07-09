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
        public IMeetingEventReadService _readService;

        public CreateNoteHandler(INoteRepository repository, IMeetingEventReadService eventReadService)
        {
            _repository = repository;
            _readService = eventReadService;
        }


        public async Task Handle(CreateNote request, CancellationToken cancellationToken)
        {

            var(Id, Title, Content, User) = request;

            if(await _readService.ExistsByIdAsync(request.Id))
            {
                throw new NoteAlreadayExistsException(request.Id);
            }
            Note note = new(Id, Title, Content, User); 

            await _repository.Add(note);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
