using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class OrganizationUsersCollectionCannotBeEmptyException : VirtualOfficeException
    {
        public OrganizationUsersCollectionCannotBeEmptyException() : base($"Organization Users Collection cannot be empty")
        {
        }
    }
}
