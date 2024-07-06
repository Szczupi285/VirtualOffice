using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class TooLongPublicChatRoomNameException : VirtualOfficeException
    {
        string Value;
        public TooLongPublicChatRoomNameException(string value, ushort length) : base($"PublicChatRoomName: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}
