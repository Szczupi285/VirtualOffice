using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoom;
using VirtualOffice.Domain.ValueObjects.ChatRoom;

namespace DomainUnitTests
{
    public class PublicChatRoomUnitTests
    {
        private HashSet<ApplicationUser> _Participants;
        private SortedSet<Message> _Messages = new SortedSet<Message>();
        private ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
        private ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
        private ApplicationUser user2 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
        private PublicChatRoom _ChatRoom;

        public PublicChatRoomUnitTests()
        {
            HashSet<ApplicationUser> participants = new HashSet<ApplicationUser>();
            participants.Add(user);
            participants.Add(user1);
            _Participants = participants;
            _ChatRoom = new PublicChatRoom(Guid.NewGuid(), _Participants, _Messages, "Name");
        }

        [Fact]
        public void InvalidParticipantNumber_OneParticipant_ShouldThrowInvalidChatRoomParticipantsException()
        {
            _Participants.Remove(user);
            Assert.Throws<InvalidChatRoomParticipantsException>(() => new PrivateChatRoom(Guid.NewGuid(), _Participants, _Messages));
        }
        [Fact]
        public void InvalidParticipantNumber_NoParticipants_ShouldThrowInvalidChatRoomParticipantsException()
        {
            _Participants.Remove(user1);
            _Participants.Remove(user2);
            Assert.Throws<InvalidChatRoomParticipantsException>(() => new PrivateChatRoom(Guid.NewGuid(), _Participants, _Messages));
        }
        [Fact]
        public void NullMessages_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new PrivateChatRoom(Guid.NewGuid(), null, _Messages));
        }
        [Fact]
        public void NullParticipants_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new PrivateChatRoom(Guid.NewGuid(), _Participants, null));
        }

        #region ChatRoomName
        [Fact]
        public void CharRoomName_ShouldReturnEmptyPublicChatRoomNameException()
        {
            Assert.Throws<EmptyPublicChatRoomNameException>(()
                => new PublicChatRoomName(String.Empty));
        }
        [Fact]
        public void ChatRoomName_Null_ShouldReturnEmptyPublicChatRoomNameException()
        {
            Assert.Throws<EmptyPublicChatRoomNameException>(()
                => new PublicChatRoomName(null));
        }

        [Fact]
        public void InvalidPublicChatRoomName_ShouldReturnInvalidPublicChatRoomNameException()
        {
            string input = new string('a', 61);
            Assert.Throws<TooLongPublicChatRoomNameException>(()
                => new PublicChatRoomName(input));
        }
        [Fact]
        public void ValidPublicChatRoomName_ShouldNotThrowException()
        {
            string input = new string('a', 60);
            PublicChatRoomName name = input;
        }

        [Theory]
        [InlineData("ChatRoom153:G")]
        [InlineData("PublicChatRoom")]
        [InlineData("O")]
        public void ValidPublicChatRoomName_StringShouldMatch(string input)
        {
            PublicChatRoomName value = input;
            Assert.Equal(input, value);
        }
        #endregion
    }
}
