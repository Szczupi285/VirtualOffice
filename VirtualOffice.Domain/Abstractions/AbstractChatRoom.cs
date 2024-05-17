using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoom;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AbstractChatRoom
    {
        public ChatRoomId Id { get; }

        public HashSet<ApplicationUser> _Participants { get; private set; }

        public SortedSet<Message> _Messages { get; private set; }

        protected AbstractChatRoom(ChatRoomId id, HashSet<ApplicationUser> participants, SortedSet<Message> messages)
        {
            if (messages is null)
                throw new ArgumentNullException($"{nameof(messages)} cannot be null");
            else if(participants is null)
                throw new ArgumentNullException($"{nameof(participants)} cannot be null");
            else if (participants.Count < 2)
                throw new InvalidChatRoomParticipantsException();
            Id = id;
            _Participants = participants;
            _Messages = messages;
        }

        public void SendMessage(ApplicationUser sender, string content)
        {
            Message message = new Message(Guid.NewGuid(), sender, content);
            _Messages.Add(message);
        }

    }
}
