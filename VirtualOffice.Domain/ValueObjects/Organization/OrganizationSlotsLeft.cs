using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Organization;

namespace VirtualOffice.Domain.ValueObjects.Organization
{
    public sealed record OrganizationSlotsLeft
    {
        public ushort? Value { get; private set; }

        private readonly ushort? _userLimit;
        private readonly ushort _usedSlots;

        public OrganizationSlotsLeft(ushort? userLimit, ushort usedSlots)
        {
            _userLimit = userLimit;
            _usedSlots = usedSlots;
            CalculateSlotsLeft();   
        }

        private void CalculateSlotsLeft()
        {
            if (_userLimit is null)
                Value = null;
            else
            {
                var slotsLeft = _userLimit - _usedSlots;
                if (slotsLeft <= 0)
                    throw new OrganizationNotEnoughSlotsException();
                else
                    Value = (ushort)slotsLeft;
            }
        }

        public static implicit operator ushort?(OrganizationSlotsLeft userLimit)
            => userLimit.Value;

        public static implicit operator OrganizationSlotsLeft(ushort? userLimit)
            => new(userLimit);
    }
}
