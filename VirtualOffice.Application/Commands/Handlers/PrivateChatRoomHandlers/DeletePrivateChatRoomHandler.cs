using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.PrivateChatRoomCommands;
using VirtualOffice.Application.Exceptions.PrivateChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PrivateChatRoomHandlers
{
    public class DeletePrivateChatRoomHandler : IRequestHandler<DeletePrivateChatRoom>
    {
        public IPrivateChatRoomRepository _repository;
        public IPrivateChatRoomReadService _readService;

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