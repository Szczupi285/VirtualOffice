using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.DomainEvents.AbstractChatRoomEvents;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoom;
using VirtualOffice.Domain.Exceptions.Office;

namespace DomainUnitTests
{
    public class PrivateChatRoomUnitTests
    {
         private HashSet<ApplicationUser> _Participants;
         private SortedSet<Message> _Messages = new SortedSet<Message>();
         private ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
         private ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
         private ApplicationUser UserNotAdded = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
         private PrivateChatRoom _ChatRoom;

        public PrivateChatRoomUnitTests()
        {
            HashSet<ApplicationUser> participants = new HashSet<ApplicationUser>();
            participants.Add(user);
            participants.Add(user1);
            _Participants = participants;
            _ChatRoom = new PrivateChatRoom(Guid.NewGuid(), _Participants, _Messages);
        }

        #region Events
        [Fact]
        public void SendMessage_ShouldRaiseChatRoomMessageSend()
        {
            _ChatRoom.SendMessage(user, "message");
            var Event = _ChatRoom.Events.OfType<ChatRoomMessageSent>().Single();
        }
        [Fact]
        public void SendMessage_ShouldRaiseChatRoomMessageSend_EventRoomShouldEqual()
        {
            _ChatRoom.SendMessage(user, "message");
            var Event = _ChatRoom.Events.OfType<ChatRoomMessageSent>().Single();
            Assert.Equal(_ChatRoom, Event.room);
        }
        [Fact]
        public void SendMessage_ShouldRaiseChatRoomMessageSend_EventMessageContentShouldEqual()
        {
            _ChatRoom.SendMessage(user, "message");
            var Event = _ChatRoom.Events.OfType<ChatRoomMessageSent>().Single();
            Assert.Equal("message", Event.message.Content);
            Assert.Equal(user, Event.message.Sender);
        }
        [Fact]
        public void SendMessage_ShouldRaiseChatRoomMessageSend_EventMessageSenderShouldEqual()
        {
            _ChatRoom.SendMessage(user, "message");
            var Event = _ChatRoom.Events.OfType<ChatRoomMessageSent>().Single();
            Assert.Equal(user, Event.message.Sender);
        }
        #endregion

        #region Constructors
        [Fact]
        public void InvalidParticipantNumber_ThreeParticipants_ShouldThrowInvalidParticipantsException()
        {
            _Participants.Add(UserNotAdded);
            Assert.Throws<InvalidPrivateRoomParticipantsException>(() => new PrivateChatRoom(Guid.NewGuid(), _Participants, _Messages));
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
            _Participants.Remove(user);
            _Participants.Remove(user1);
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

        #region Methods
        [Fact]
        public void SendMessage_ShouldAddMessage()
        {
            _ChatRoom.SendMessage(user1, "Content");

            Assert.Single(_ChatRoom._Messages);
        }
        [Fact]
        public void SendMessage_ShouldThrowUserIsNotAParticipantOfThisChat()
        {
            ApplicationUser NotAParticipant = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");

            Assert.Throws<UserIsNotAParticipantOfThisChatException>(() => _ChatRoom.SendMessage(NotAParticipant, "content"));
        }

        [Fact]
        public void GetParticipantById_ParticipantFound_ShouldReturnUser()
        {
            ApplicationUser foundMember = _ChatRoom.GetParticipantById(user.Id);

            Assert.Equal(user, foundMember);
        }
        [Fact]
        public void GetParticipantById_ParticipantNotFound_ShouldThrowOfficeMemberNotFoundException()
        {
            Assert.Throws<ChatRoomParticipantNotFoundException>(()
                => _ChatRoom.GetParticipantById(Guid.NewGuid()));
        }
        #endregion

    }
}
