using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.PublicChatRoomCommands
{
    public record CreatePublicChatRoom(HashSet<ApplicationUser> Participants, SortedSet<Message> Messages, string Name) : IRequest;
}
