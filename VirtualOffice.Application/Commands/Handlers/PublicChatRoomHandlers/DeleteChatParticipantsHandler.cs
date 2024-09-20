﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.Exceptions.PublicChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers
{
    public class DeleteChatParticipantsHandler : IRequestHandler<DeleteChatParticipants>
    {
        public IPublicChatRoomRepository _repository;
        public IPublicChatRoomReadService _readService;

        public DeleteChatParticipantsHandler(IPublicChatRoomRepository repository, IPublicChatRoomReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeleteChatParticipants request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PublicChatRoomDoesNotExistException(request.Id);

            var pcr = await _repository.GetById(request.Id);

            pcr.RemoveRangeParticipants(request.Participants);

            await _repository.UpdateAsync(pcr);
        }
    }
}