using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class ChatRoomCannotHaveNoParticipants : VirtualOfficeException
    {
        public ChatRoomCannotHaveNoParticipants() : base("Chat room must have at least one participant.")
        {
        }
    }
}
