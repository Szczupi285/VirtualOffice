﻿using MediatR;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.Exceptions.PublicChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers
{
    internal sealed class DeleteChatParticipantsHandler : IRequestHandler<DeleteChatParticipants>
    {
        private readonly IPublicChatRoomRepository _repository;
        private readonly IPublicChatRoomReadService _readService;

        public DeleteChatParticipantsHandler(IPublicChatRoomRepository repository, IPublicChatRoomReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeleteChatParticipants request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PublicChatRoomDoesNotExistException(request.Id);

            var pcr = await _repository.GetByIdAsync(request.Id);

            pcr.RemoveRangeParticipants(request.Participants);

            await _repository.UpdateAsync(pcr);
        }
    }
}