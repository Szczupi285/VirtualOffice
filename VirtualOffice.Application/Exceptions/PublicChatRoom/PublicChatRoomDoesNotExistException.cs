using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.PublicChatRoom
{
    public class PublicChatRoomDoesNotExistException : VirtualOfficeException
    {
        public PublicChatRoomDoesNotExistException(Guid Id) : base($"Public chat room with Id: {Id} does not exist")
        {
        }
    }
}
