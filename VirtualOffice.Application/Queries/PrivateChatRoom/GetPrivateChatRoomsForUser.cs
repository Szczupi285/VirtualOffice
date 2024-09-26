using MediatR;
using VirtualOffice.Application.DTO.PrivateChatRoom;

namespace VirtualOffice.Application.Queries.PrivateChatRoom
{
    public record GetPrivateChatRoomsForUser(Guid UserId) : IRequest<IEnumerable<PrivateChatRoomDTO>>;
}
