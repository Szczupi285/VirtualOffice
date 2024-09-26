using MediatR;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.PublicChatRoom
{
    public record GetPublicChatRoomMessages(Guid PublicChatRoomId) : IRequest<IEnumerable<MessageDTO>>;
}
