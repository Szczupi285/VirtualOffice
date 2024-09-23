using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Repositories
{
    public class PrivateChatRoomNotFoundException : VirtualOfficeException
    {
        public PrivateChatRoomNotFoundException(Guid guid) : base($"Private chat room with Id: {guid} has not been found")
        {
        }
    }
}