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
    public class UpdatePublicChatRoomNameHandler : IRequestHandler<UpdatePublicChatRoomName>
    {
        public IPublicChatRoomRepository _repository;
        public IPublicChatRoomReadService _readService;

        public UpdatePublicChatRoomNameHandler(IPublicChatRoomRepository repository, IPublicChatRoomReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdatePublicChatRoomName request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PublicChatRoomDoesNotExistException(request.Id);

            var pcr = await _repository.GetByIdAsync(request.Id);

            pcr.SetName(request.Name);

            await _repository.UpdateAsync(pcr);
        }
    }
}