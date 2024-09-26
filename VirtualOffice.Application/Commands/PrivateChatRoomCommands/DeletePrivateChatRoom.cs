using MediatR;

namespace VirtualOffice.Application.Commands.PrivateChatRoomCommands
{
    public record DeletePrivateChatRoom(Guid Id) : IRequest;

}
