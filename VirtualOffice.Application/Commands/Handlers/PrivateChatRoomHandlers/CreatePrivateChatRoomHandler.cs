using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PrivateChatRoomCommands;
using VirtualOffice.Application.Exceptions.PrivateChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PrivateChatRoomHandlers
{
    public class CreatePrivateChatRoomHandlerHandler : IRequestHandler<CreatePrivateChatRoom>
    {

        public IPrivateChatRoomRepository _repository;
        public IPrivateChatRoomReadService _readService;

        public CreatePrivateChatRoomHandlerHandler(IPrivateChatRoomRepository repository, IPrivateChatRoomReadService noteReadService)
        {
            _repository = repository;
            _readService = noteReadService;
        }


        public async Task Handle(CreatePrivateChatRoom request, CancellationToken cancellationToken)
        {

            if (await _readService.ExistsByIdAsync(request.Id))
                throw new PrivateChatRoomAlreadyExistException(request.Id);

            PrivateChatRoom pcr = new(request.Id, request.Participants, request.Messages);

            await _repository.Add(pcr);
            await _repository.SaveAsync(cancellationToken);
        }
    }
    
}
