using MediatR;

namespace VirtualOffice.Application.Commands.PublicChatRoomCommands
{
    public record DeletePublicChatRoom(Guid Id) : IRequest;
}
