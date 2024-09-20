using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.Exceptions.ApplicationUser;
using VirtualOffice.Application.Exceptions.PublicChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers
{
    public class SendPublicMessageHandler : IRequestHandler<SendPublicMessage>
    {
        public IPublicChatRoomRepository _repository;
        public IPublicChatRoomReadService _readService;
        public IUserReadService _userReadService;
        public IUserRepository _userRepository;

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

            var pcr = await _repository.GetById(request.ChatRoomId);
            var user = await _userRepository.GetById(request.UserId);

            pcr.SendMessage(user, request.Content);

            await _repository.UpdateAsync(pcr);
        }
    }
}