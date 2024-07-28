using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.PrivateChatRoom
{
    public record GetPrivateChatRoomsForUser(Guid UserId) : IRequest<IEnumerable<PrivateChatRoomDTO>>;
}
