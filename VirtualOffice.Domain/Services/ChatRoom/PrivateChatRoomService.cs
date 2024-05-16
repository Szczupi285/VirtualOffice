using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoomService;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Services
{
    public class PrivateChatRoomService
    {
        public HashSet<PrivateChatRoom> _PrivateChatRooms { get; private set; }

        public PrivateChatRoomService(HashSet<PrivateChatRoom> privateChatRooms)
        {
            _PrivateChatRooms = privateChatRooms;
        }

        public void AddChatRoom(PrivateChatRoom chatRoom) => _PrivateChatRooms.Add(chatRoom);

        public PrivateChatRoom GetChatRoom(ChatRoomId id) => _PrivateChatRooms.FirstOrDefault(x => x.Id == id) ?? throw new ChatRoomIdNotFoundException(id);

        public List<PrivateChatRoom> GetChatRoomsForUser(ApplicationUser user)
        {
            return _PrivateChatRooms.Where(c => c._Participants.Any(p => p.Id == user.Id)).ToList();
        }
    }
}
