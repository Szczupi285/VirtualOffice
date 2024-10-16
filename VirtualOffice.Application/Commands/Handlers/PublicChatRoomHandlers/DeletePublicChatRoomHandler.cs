using MediatR;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.Exceptions.PublicChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers
{
    internal sealed class DeletePublicChatRoomHandler : IRequest<DeletePublicChatRoom>
    {
        private readonly IPublicChatRoomRepository _repository;
        private readonly IPublicChatRoomReadService _readService;

        public DeletePublicChatRoomHandler(IPublicChatRoomRepository repository, IPublicChatRoomReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeletePublicChatRoom request, CancellationToken cancellationToken)
        {
            if (!await _readService.ExistsByIdAsync(request.Id))
                throw new PublicChatRoomDoesNotExistException(request.Id);

            var entity = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(entity);
        }
    }
}