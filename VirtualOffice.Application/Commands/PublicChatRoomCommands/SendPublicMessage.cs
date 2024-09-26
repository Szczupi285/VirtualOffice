using MediatR;

namespace VirtualOffice.Application.Commands.PublicChatRoomCommands
{
    public record SendPublicMessage(Guid ChatRoomId, Guid UserId, string Content) : IRequest;
}
