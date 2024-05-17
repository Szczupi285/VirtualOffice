using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoom;

namespace DomainUnitTests
{
    public class PrivateChatRoomUnitTests
    {
         private HashSet<ApplicationUser> participants = new HashSet<ApplicationUser>();
         private SortedSet<Message> messages = new SortedSet<Message>();
         ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
         ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
         ApplicationUser user2 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");

        [Fact]
        public void InvalidParticipantNumber_ThreeParticipants_ShouldThrowInvalidParticipantsException()
        {
            participants.Add(user);
            participants.Add(user1);
            participants.Add(user2);
            Assert.Throws<InvalidPrivateRoomParticipantsException>(() => new PrivateChatRoom(Guid.NewGuid(), participants, messages));
        }
        [Fact]
        public void InvalidParticipantNumber_OneParticipant_ShouldThrowInvalidChatRoomParticipantsException()
        {
            participants.Add(user);
            Assert.Throws<InvalidChatRoomParticipantsException>(() => new PrivateChatRoom(Guid.NewGuid(), participants, messages));
        }
        [Fact]
        public void InvalidParticipantNumber_NoParticipants_ShouldThrowInvalidChatRoomParticipantsException()
        {
            Assert.Throws<InvalidChatRoomParticipantsException>(() => new PrivateChatRoom(Guid.NewGuid(), participants, messages));
        }
        [Fact]
        public void NullMessages_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new PrivateChatRoom(Guid.NewGuid(), null, messages));
        }
        [Fact]
        public void NullParticipants_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new PrivateChatRoom(Guid.NewGuid(), participants, null));
        }

    }
}
