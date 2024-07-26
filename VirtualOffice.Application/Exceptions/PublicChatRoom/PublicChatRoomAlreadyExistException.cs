using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.PublicChatRoom
{
    public class PublicChatRoomAlreadyExistException : VirtualOfficeException
    {
        public PublicChatRoomAlreadyExistException(Guid id) : base($"Public chat room with Id: {id} already exist")
        {
        }
    }
}
