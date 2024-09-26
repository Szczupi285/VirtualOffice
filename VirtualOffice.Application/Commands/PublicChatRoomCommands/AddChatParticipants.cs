using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.PublicChatRoomCommands
{
    public record AddChatParticipants(Guid Id, HashSet<ApplicationUser> Participants) : IRequest;
}
