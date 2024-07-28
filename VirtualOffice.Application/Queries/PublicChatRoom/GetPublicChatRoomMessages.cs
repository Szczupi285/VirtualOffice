using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO;
using VirtualOffice.Domain.ValueObjects.Message;

namespace VirtualOffice.Application.Queries.PublicChatRoom
{
    public record GetPublicChatRoomMessages(Guid PublicChatRoomId) : IRequest<IEnumerable<MessageDTO>>;
}
