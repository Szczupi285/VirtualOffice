﻿using MediatR;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.Exceptions.PublicChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers
{
    public class CreatePublicChatRoomHandler : IRequest<CreatePublicChatRoom>
    {
        public IPublicChatRoomRepository _repository;

        public CreatePublicChatRoomHandler(IPublicChatRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreatePublicChatRoom request, CancellationToken cancellationToken)
        {
            PublicChatRoom pcr = new(Guid.NewGuid(), request.Participants, request.Messages, request.Name);

            await _repository.Add(pcr);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
