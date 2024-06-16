using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class InvalidChatRoomParticipantsException : VirtualOfficeException
    {
        public InvalidChatRoomParticipantsException() : base($"Chat room must have at least 2 participants while initializating")
        {
        }
    }
}
