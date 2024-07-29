using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Application.DTO.PrivateChatRoom
{
    public class PrivateChatRoomDTO
    {
        public Guid ChatRoomId { get; init; }
        public string ChatParticipantName { get; init; }
        public string ChatParticipantSurname { get; init; }
    }
}
