using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class OrganizationNotEnoughSlotsException : VirtualOfficeException
    {
        public OrganizationNotEnoughSlotsException() : base($"Current Subscription does not support that many slots")
        {
        }
    }
}
