using MediatR;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers
{
    internal sealed class CreatePublicChatRoomHandler : IRequest<CreatePublicChatRoom>
    {
        private readonly IPublicChatRoomRepository _repository;

        public CreatePublicChatRoomHandler(IPublicChatRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreatePublicChatRoom request, CancellationToken cancellationToken)
        {
            PublicChatRoom pcr = new(Guid.NewGuid(), request.Participants, request.Messages, request.Name);

            await _repository.AddAsync(pcr);
        }
    }
}