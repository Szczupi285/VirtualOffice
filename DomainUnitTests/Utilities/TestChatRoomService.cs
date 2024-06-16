using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;

namespace DomainUnitTests.Utilities
{
    internal class TestChatRoomService : ChatRoomService<TestChatRoom>
    {
        public TestChatRoomService(HashSet<TestChatRoom> chatRooms) : base(chatRooms) { }
    }
}
