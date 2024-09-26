using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoomService;
using VirtualOffice.Domain.Services.ChatRoom;
namespace DomainUnitTests
{
    public class PublicChatRoomServiceTests
    {
        private HashSet<PublicChatRoom> _chatRooms;
        private PublicChatRoom _chatRoom;
        private PublicChatRoomService _chatRoomService;
        private HashSet<ApplicationUser> _Participants;
        private SortedSet<Message> _Messages = new SortedSet<Message>();
        private ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
        private ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
        private ApplicationUser UserNotAdded = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
        private Guid _chatRoomId = Guid.NewGuid();
        public PublicChatRoomServiceTests()
        {
            HashSet<ApplicationUser> participants = new HashSet<ApplicationUser>();
            participants.Add(user);
            participants.Add(user1);
            _Participants = participants;
            _chatRoom = new PublicChatRoom(_chatRoomId, _Participants, _Messages, "name");
            _chatRooms = new HashSet<PublicChatRoom>();
            _chatRoomService = new PublicChatRoomService(_chatRooms);
        }

        [Fact]
        public void AddChatRoom_ShouldAddChatRoom()
        {
            _chatRoomService.AddChatRoom(_chatRoom);
            Assert.Contains(_chatRoom, _chatRooms);
        }

        [Fact]
        public void DeleteChatRoom_ShouldRemoveChatRoom()
        {
            _chatRooms.Add(_chatRoom);
            Assert.Contains(_chatRoom, _chatRooms);
            _chatRoomService.DeleteChatRoom(_chatRoomId);
            Assert.DoesNotContain(_chatRoom, _chatRooms);
        }

        [Fact]
        public void DeleteChatRoom_NonExistentId_ShouldThrowException()
        {
            Assert.Throws<ChatRoomIdNotFoundException>(() => _chatRoomService.DeleteChatRoom(Guid.NewGuid()));
        }

        [Fact]
        public void GetChatRoom_ShouldReturnChatRoom()
        {
            _chatRooms.Add(_chatRoom);
            var result = _chatRoomService.GetChatRoom(_chatRoomId);
            Assert.Equal(_chatRoom, result);
        }

        [Fact]
        public void GetChatRoom_NonExistentId_ShouldThrowException()
        {
            Assert.Throws<ChatRoomIdNotFoundException>(() => _chatRoomService.GetChatRoom(Guid.NewGuid()));
        }

        [Fact]
        public void GetChatRoomsForUser_ShouldReturnUserChatRooms()
        {
            PublicChatRoom testChat = new PublicChatRoom(Guid.NewGuid(), _Participants, _Messages, "name");
            _chatRoomService.AddChatRoom(_chatRoom);
            _chatRoomService.AddChatRoom(testChat);
            var result = _chatRoomService.GetChatRoomsForUser(user);
            Assert.Contains(_chatRoom, result);
            Assert.Contains(testChat, result);
        }
        [Fact]
        public void GetChatRoomsForUser_ShouldReturnEmptyList()
        {
            PublicChatRoom testChat = new PublicChatRoom(Guid.NewGuid(), _Participants, _Messages, "name");
            _chatRooms.Add(testChat);
            var result = _chatRoomService.GetChatRoomsForUser(UserNotAdded);
            Assert.Empty(result);

        }
    }
}



