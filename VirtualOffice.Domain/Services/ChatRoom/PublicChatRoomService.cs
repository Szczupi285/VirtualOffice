using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.Services.ChatRoom
{
    public class PublicChatRoomService : ChatRoomService<PublicChatRoom>
    {
        public PublicChatRoomService(HashSet<PublicChatRoom> publicChatRooms) : base(publicChatRooms)
        {
        }
    }
}