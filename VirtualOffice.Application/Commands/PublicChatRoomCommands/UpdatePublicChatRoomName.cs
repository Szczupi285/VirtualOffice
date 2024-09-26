using MediatR;

namespace VirtualOffice.Application.Commands.PublicChatRoomCommands
{
    public record UpdatePublicChatRoomName(Guid Id, string Name) : IRequest;
}
