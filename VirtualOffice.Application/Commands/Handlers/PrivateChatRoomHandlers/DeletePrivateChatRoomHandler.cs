using MediatR;
using VirtualOffice.Application.Commands.PrivateChatRoomCommands;
using VirtualOffice.Application.Exceptions.PrivateChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PrivateChatRoomHandlers
{
    internal sealed class DeletePrivateChatRoomHandler : IRequestHandler<DeletePrivateChatRoom>
    {
        private readonly IPrivateChatRoomRepository _repository;
        private readonly IPrivateChatRoomReadService _readService;

        public DeletePrivateChatRoomHandler(IPrivateChatRoomRepository repository, IPrivateChatRoomReadService noteReadService)
        {
            _repository = repository;
            _readService = noteReadService;
        }

        public async Task Handle(DeletePrivateChatRoom request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PrivateChatRoomDoesNotExistException(request.Id);

            var entity = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(entity);
        }
    }
}