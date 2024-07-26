using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ChatRoom;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace VirtualOffice.Domain.Entities
{
    public class PrivateChatRoom : AbstractChatRoom
    {
        public PrivateChatRoom(ChatRoomId id, HashSet<ApplicationUser> participants, SortedSet<Message> messages) : base(id, participants, messages)
        {
            if (participants.Count != 2)
                throw new InvalidPrivateRoomParticipantsException();
        }
    }
}
