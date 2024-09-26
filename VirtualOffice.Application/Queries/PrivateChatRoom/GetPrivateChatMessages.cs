using MediatR;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.PrivateChatRoom
{
    public record GetPrivateChatMessages(Guid PrivateChatRoomId) : IRequest<IEnumerable<MessageDTO>>;
}
