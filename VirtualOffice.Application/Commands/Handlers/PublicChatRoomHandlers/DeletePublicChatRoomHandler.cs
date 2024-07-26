using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.Exceptions.PublicChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers
{
    public class DeletePublicChatRoomHandler : IRequest<DeletePublicChatRoom>
    {
        public IPublicChatRoomRepository _repository;
        public IPublicChatRoomReadService _readService;

        public DeletePublicChatRoomHandler(IPublicChatRoomRepository repository, IPublicChatRoomReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(DeletePublicChatRoom request, CancellationToken cancellationToken)
        {
            if(!await _readService.ExistsByIdAsync(request.Id))
                throw new PublicChatRoomDoesNotExistException(request.Id);

            await _repository.Delete(request.Id);
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
