using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class InvalidOrganizationUsedSlotsException : VirtualOfficeException
    {
        public InvalidOrganizationUsedSlotsException() : base("OrganizationUsedSlots cannot be equal to '0'.")
        {
        }
    }
}
