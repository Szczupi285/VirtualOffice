using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.ApplicationUser
{
    public class UserDoesNotExistException : VirtualOfficeException
    {
        public UserDoesNotExistException(Guid id) : base($"User with Id: {id} does not exist")
        {
        }
    }
}
