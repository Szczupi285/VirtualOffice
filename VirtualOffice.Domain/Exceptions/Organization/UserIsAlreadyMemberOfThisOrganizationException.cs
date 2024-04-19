using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class UserIsAlreadyMemberOfThisOrganizationException : VirtualOfficeException
    {
        Guid Value;
        public UserIsAlreadyMemberOfThisOrganizationException(Guid value) : base($"User with Id: {value} is already member of this organization")
        {
            Value = value;
        }
    }
}
