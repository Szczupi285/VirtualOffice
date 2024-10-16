﻿using MediatR;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.Exceptions.ApplicationUser;
using VirtualOffice.Application.Exceptions.PublicChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers
{
    internal sealed class SendPublicMessageHandler : IRequestHandler<SendPublicMessage>
    {
        private readonly IPublicChatRoomRepository _repository;
        private readonly IPublicChatRoomReadService _readService;
        private readonly IUserReadService _userReadService;
        private readonly IUserRepository _userRepository;

        public SendPublicMessageHandler(IPublicChatRoomRepository repository, IPublicChatRoomReadService noteReadService, IUserReadService userReadService, IUserRepository userRepository)
        {
            _repository = repository;
            _readService = noteReadService;
            _userReadService = userReadService;
            _userRepository = userRepository;
        }

        public async Task Handle(SendPublicMessage request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.ChatRoomId))
                throw new PublicChatRoomDoesNotExistException(request.ChatRoomId);
            if (!await _userReadService.ExistsByIdAsync(request.UserId))
                throw new UserDoesNotExistException(request.UserId);

            var pcr = await _repository.GetByIdAsync(request.ChatRoomId);
            var user = await _userRepository.GetByIdAsync(request.UserId);

            pcr.SendMessage(user, request.Content);

            await _repository.UpdateAsync(pcr);
        }
    }
}