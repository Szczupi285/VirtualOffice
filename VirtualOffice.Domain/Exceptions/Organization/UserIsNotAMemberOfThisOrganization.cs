using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class UserIsNotAMemberOfThisOrganization : VirtualOfficeException
    {
        Guid Value;
        public UserIsNotAMemberOfThisOrganization(Guid value) : base($"User with Id: {value} is not a member of this organization")
        {
            Value = value;
        }
    }
}
