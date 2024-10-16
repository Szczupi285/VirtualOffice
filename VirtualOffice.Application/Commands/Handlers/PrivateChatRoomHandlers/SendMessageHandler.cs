using MediatR;
using VirtualOffice.Application.Commands.PrivateChatRoomCommands;
using VirtualOffice.Application.Exceptions.ApplicationUser;
using VirtualOffice.Application.Exceptions.PrivateChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PrivateChatRoomHandlers
{
    internal sealed class SendMessageHandler : IRequestHandler<SendMessage>
    {
        private readonly IPrivateChatRoomRepository _repository;
        private readonly IPrivateChatRoomReadService _readService;
        private readonly IUserReadService _userReadService;
        private readonly IUserRepository _userRepository;

        public SendMessageHandler(IPrivateChatRoomRepository repository, IPrivateChatRoomReadService readService, IUserRepository userRepository, IUserReadService userReadService)
        {
            _repository = repository;
            _readService = readService;
            _userReadService = userReadService;
            _userRepository = userRepository;
        }

        public async Task Handle(SendMessage request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.ChatRoomId))
                throw new PrivateChatRoomDoesNotExistException(request.ChatRoomId);
            if (!await _userReadService.ExistsByIdAsync(request.UserId))
                throw new UserDoesNotExistException(request.UserId);

            var pcr = await _repository.GetByIdAsync(request.ChatRoomId);
            var user = await _userRepository.GetByIdAsync(request.UserId);

            pcr.SendMessage(user, request.Content);

            await _repository.UpdateAsync(pcr);
        }
    }
}