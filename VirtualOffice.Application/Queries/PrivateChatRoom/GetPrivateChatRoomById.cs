using MediatR;
using VirtualOffice.Application.Models;

namespace VirtualOffice.Application.Queries.PrivateChatRoom
{
    public record GetPrivateChatRoom(Guid PrivateChatRoomId) : IRequest<IEnumerable<PrivateChatRoomReadModel>>;
}