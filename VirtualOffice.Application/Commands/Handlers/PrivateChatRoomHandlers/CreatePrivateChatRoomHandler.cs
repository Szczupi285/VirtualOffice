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
    public class CreatePrivateChatRoomHandler : IRequestHandler<CreatePrivateChatRoom>
    {
        public IPrivateChatRoomRepository _repository;

        public CreatePrivateChatRoomHandler(IPrivateChatRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreatePrivateChatRoom request, CancellationToken cancellationToken)
        {
            PrivateChatRoom pcr = new(Guid.NewGuid(), request.Participants, request.Messages);

            await _repository.AddAsync(pcr);
        }
    }
}