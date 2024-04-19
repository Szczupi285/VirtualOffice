using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract record AbstractRecordId
    {
        public Guid Value { get; }

        public AbstractRecordId(Guid value, VirtualOfficeException virtOfficeEx)
        {
            if (value == Guid.Empty)
                throw virtOfficeEx;

            Value = value;
        }

        public static implicit operator Guid(AbstractRecordId id)
            => id.Value;
        // since we can't create new instances in abstract class we have to make implicit conversion in derived record

    }
}
