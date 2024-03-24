using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Domain.Exceptions.Organization;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.ValueObjects.Office
{
    public sealed record OfficeId
    {
        public Guid Value { get; }

        public OfficeId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyOfficeIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(OfficeId id)
            => id.Value;

        public static implicit operator OfficeId(Guid id)
            => new(id);

    }
}
