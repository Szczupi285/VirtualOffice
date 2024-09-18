using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.Services.ChatRoom
{
    public class PrivateChatRoomService : ChatRoomService<PrivateChatRoom>
    {
        public PrivateChatRoomService(HashSet<PrivateChatRoom> privateChatRooms) : base(privateChatRooms)
        {
        }
    }
}