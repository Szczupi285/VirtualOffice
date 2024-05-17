using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoomService;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Services.ChatRoom
{
    public class PublicChatRoomService
    {
        public HashSet<PublicChatRoom> _PublicChatRooms { get; private set; }

        public PublicChatRoomService(HashSet<PublicChatRoom> PublicChatRooms)
        {
            _PublicChatRooms = PublicChatRooms;
        }

        public void AddChatRoom(PublicChatRoom chatRoom) => _PublicChatRooms.Add(chatRoom);

        public PublicChatRoom GetChatRoom(ChatRoomId id) => _PublicChatRooms.FirstOrDefault(x => x.Id == id) ?? throw new ChatRoomIdNotFoundException(id);

        public List<PublicChatRoom> GetChatRoomsForUser(ApplicationUser user)
        {
            return _PublicChatRooms.Where(c => c._Participants.Any(p => p.Id == user.Id)).ToList();
        }
    }
}
