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
        private ApplicationUser userNotAdded1 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
        private ApplicationUser userNotAdded2 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
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
        public void ParticipantsProperty_IsHashSetType()
        {
            // we have to check if participants property is of type HashSet since we don't check for duplicates while adding participants.
            // Remember to refactor AddParticipant method if we decide to change data structure
            Assert.IsType<HashSet<ApplicationUser>>(_ChatRoom._Participants);
        }

        #region Constructors
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
            _Participants.Remove(userNotAdded1);
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
        #endregion

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

        #region Methods

        [Fact]
        public void AddParticipant_ParticipantAdded()
        {
            _ChatRoom.AddParticipant(userNotAdded1);
            Assert.Contains(user, _ChatRoom._Participants);
        }
        [Fact]
        public void AddParticipantsRange_ParticipantsAdded()
        {
            _ChatRoom.AddRangeParticipants(new List<ApplicationUser>() { userNotAdded1, userNotAdded2});
            Assert.Contains(user, _ChatRoom._Participants);
        }

        [Fact]
        public void RemoveParticipant_ParticipantRemoved()
        {
            _ChatRoom.RemoveParticipant(user);
            Assert.DoesNotContain(user, _ChatRoom._Participants);
        }
        [Fact]
        public void RemoveParticipant_NotAParticipant_ShouldThrowUserIsNotAParticipantOfThisChat()
        {
            Assert.Throws<UserIsNotAParticipantOfThisChat>(() => _ChatRoom.RemoveParticipant(userNotAdded1)); 
        }
        [Fact]
        public void RemoveParticipant_LastParticipant_ChatRoomCannotBeEmpty()
        {
            _ChatRoom.RemoveParticipant(user1);
            Assert.Throws<ChatRoomCannotBeEmpty>(() => _ChatRoom.RemoveParticipant(user));
        }
        [Fact]
        public void RemoveParticipantsRange_ParticipantsRemoved()
        {

            ApplicationUser tempUser1 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
            ApplicationUser tempUser2 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
            ApplicationUser tempUser3 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");


            HashSet<ApplicationUser> participants = new HashSet<ApplicationUser>();
            participants.Add(tempUser1);
            participants.Add(tempUser2);
            participants.Add(tempUser3);

            var tempChatRoom = new PublicChatRoom(Guid.NewGuid(), participants, _Messages, "Name");

            tempChatRoom.RemoveRangeParticipants(new List<ApplicationUser>() { tempUser1, tempUser2 });
            Assert.DoesNotContain(tempUser1, tempChatRoom._Participants);
            Assert.DoesNotContain(tempUser2, tempChatRoom._Participants);
            Assert.Contains(tempUser3, tempChatRoom._Participants);
        }
        [Fact]
        public void SetName_NamesShouldEqual()
        {
            string a = new string('a', 5);
            _ChatRoom.SetName(a);
            Assert.Equal(a, _ChatRoom._Name);
        }


        #endregion
    }
}
