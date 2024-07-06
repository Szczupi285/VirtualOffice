using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoomService
{
    public class ChatRoomIdNotFoundException : VirtualOfficeException
    {
        Guid Id;
        public ChatRoomIdNotFoundException(Guid id) : base($"Chat Room with Id: {id} Has not been found")
        {
            Id = id;
        }
    }
}
