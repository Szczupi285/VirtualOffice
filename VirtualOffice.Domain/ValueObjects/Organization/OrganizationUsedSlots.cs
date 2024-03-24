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
        // Even thought userLimit data type is ushort, UsedSlots uses int
        // since it's possible to have subscribtion with unlimited slots.
        public uint Value { get; }

        public OrganizationUsedSlots(uint value)
        {
            if (value == 0)
                throw new InvalidOrganizationUsedSlotsException();

            Value = value;
        }

        public static implicit operator uint(OrganizationUsedSlots userLimit)
            => userLimit.Value;

        public static implicit operator OrganizationUsedSlots(uint userLimit)
            => new(userLimit);

    }
}
