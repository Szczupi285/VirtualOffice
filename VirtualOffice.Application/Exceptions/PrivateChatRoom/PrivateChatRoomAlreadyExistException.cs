using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.PrivateChatRoom
{
    public class PrivateChatRoomAlreadyExistException : VirtualOfficeException
    {
        public PrivateChatRoomAlreadyExistException(Guid id) : base($"Private chat room with Id: {id} already exist")
        {
        }
    }
}
