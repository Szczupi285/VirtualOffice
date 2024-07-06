using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class ChatRoomParticipantNotFoundException : VirtualOfficeException
    {
        string Value;

        public ChatRoomParticipantNotFoundException(string value) : base($"Cannot find office member with the provided input: {value}.")
        {
            Value = value;
        }
    }
}
