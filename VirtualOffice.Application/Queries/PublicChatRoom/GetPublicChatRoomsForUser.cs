using MediatR;
using VirtualOffice.Application.DTO.PublicChatRoom;

namespace VirtualOffice.Application.Queries.PublicChatRoom
{
    public class GetPublicChatRoomsForUser : IRequest<IEnumerable<PublicChatRoomTitleDTO>>;
}
