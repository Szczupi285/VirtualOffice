using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace DomainUnitTests.Utilities
{
    internal class TestChatRoom : AbstractChatRoom
    {
        public TestChatRoom(ChatRoomId id, HashSet<ApplicationUser> participants, SortedSet<Message> messages) : base(id, participants, messages)
        {
        }
    }
}
