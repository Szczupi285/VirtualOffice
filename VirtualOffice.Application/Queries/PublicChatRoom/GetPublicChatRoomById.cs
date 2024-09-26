using MediatR;
using VirtualOffice.Application.DTO.PublicChatRoom;

namespace VirtualOffice.Application.Queries.PublicChatRoom
{
    public record GetPublicChatRoomById(Guid Id) : IRequest<PublicChatRoomDTO>;
}
