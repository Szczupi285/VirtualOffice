using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.PrivateChatRoomCommands
{
    public record CreatePrivateChatRoom(HashSet<ApplicationUser> Participants, SortedSet<Message> Messages) : IRequest;
}
