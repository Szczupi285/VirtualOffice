using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoomService;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class ChatRoomService<T> where T : AbstractChatRoom
    {
        protected HashSet<T> _ChatRooms { get; private set; }

        protected ChatRoomService(HashSet<T> chatRooms)
        {
            _ChatRooms = chatRooms;
        }

        public void AddChatRoom(T chatRoom) => _ChatRooms.Add(chatRoom);

        public void DeleteChatRoom(ChatRoomId id) => _ChatRooms.Remove(GetChatRoom(id));

        public T GetChatRoom(ChatRoomId id) => _ChatRooms.FirstOrDefault(x => x.Id == id) ?? throw new ChatRoomIdNotFoundException(id);

        public List<T> GetChatRoomsForUser(ApplicationUser user)
        {
            return _ChatRooms.Where(c => c._Participants.Any(p => p.Id == user.Id)).ToList();
        }
    }
}