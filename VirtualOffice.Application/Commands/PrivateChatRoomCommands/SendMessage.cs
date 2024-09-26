using MediatR;

namespace VirtualOffice.Application.Commands.PrivateChatRoomCommands
{
    public record SendMessage(Guid ChatRoomId, Guid UserId, string Content) : IRequest;
}
