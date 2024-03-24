using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Organization;

namespace VirtualOffice.Domain.ValueObjects.Organization
{
    public sealed record OrganizationUserLimit
    {
        public ushort Value { get; }

        public OrganizationUserLimit(ushort value)
        {
            if (value is 0 or > 1000)
                throw new InvalidOrganizationUserLimitException(value);

            Value = value;
        }
    }
}
