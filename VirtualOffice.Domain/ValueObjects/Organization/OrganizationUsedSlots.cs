using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Organization;

namespace VirtualOffice.Domain.ValueObjects.Organization
{
    public sealed record OrganizationUsedSlots
    {
        public ushort Value { get; }

        public OrganizationUsedSlots(ushort value)
        {
            if (value == 0)
                throw new InvalidOrganizationUsedSlotsException();

            Value = value;
        }

        public static implicit operator ushort(OrganizationUsedSlots userLimit)
            => userLimit.Value;

        public static implicit operator OrganizationUsedSlots(ushort userLimit)
            => new(userLimit);

    }
}
