using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.User
{
    public class UserDoesNotExistException : VirtualOfficeException
    {
        public UserDoesNotExistException(Guid guid) : base($"User with id: {guid} does not exists")
        {
        }
    }
}
