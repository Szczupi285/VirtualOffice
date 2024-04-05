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
        public ushort? Value { get; }

        // we're throwing exception
        public OrganizationUserLimit(ushort? value)
        {
            if (value is 0 or > 1000)
                throw new InvalidOrganizationUserLimitException(value);
            Value = value;
        }

        public static implicit operator ushort?(OrganizationUserLimit userLimit)
            => userLimit.Value;

        public static implicit operator OrganizationUserLimit(ushort? userLimit)
            => new(userLimit);
    }
}
