using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoomService;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Services.ChatRoom
{
    public class PublicChatRoomService : ChatRoomService<PublicChatRoom>
    {
        public PublicChatRoomService(HashSet<PublicChatRoom> publicChatRooms) : base(publicChatRooms)
        {
        }
    }
}
