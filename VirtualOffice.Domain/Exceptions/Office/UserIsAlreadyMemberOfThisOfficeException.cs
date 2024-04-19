using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Office
{
    public class UserIsAlreadyMemberOfThisOfficeException : VirtualOfficeException
    {
        Guid Value;
        public UserIsAlreadyMemberOfThisOfficeException(Guid value) : base($"User with Id: {value} is already a member of this office")
        => Value = value;
    }
}
