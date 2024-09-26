using VirtualOffice.Domain.Abstractions;

namespace DomainUnitTests.Utilities
{
    internal class TestChatRoomService : ChatRoomService<TestChatRoom>
    {
        public TestChatRoomService(HashSet<TestChatRoom> chatRooms) : base(chatRooms) { }
    }
}
