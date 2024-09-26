using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.PublicChatRoomCommands
{
    public record DeleteChatParticipants(Guid Id, HashSet<ApplicationUser> Participants) : IRequest;
}
