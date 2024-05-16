using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AbstractChatRoom
    {
        public ChatRoomId Id { get; }  

        public List<ApplicationUser> _Participants { get; private set; }

        public SortedSet<Message> _Messages { get; private set; }

        public AbstractChatRoom(ChatRoomId id, List<ApplicationUser> participants, SortedSet<Message> messages)
        {
            Id = id;
            _Participants = participants;
            _Messages = messages;
        }


    }
}
