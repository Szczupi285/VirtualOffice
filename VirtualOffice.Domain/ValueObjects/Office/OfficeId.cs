using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Domain.Exceptions.Organization;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.ValueObjects.Office
{
    public sealed record OfficeId : AbstractRecordId
    {
        public OfficeId(Guid value) : base(value, new EmptyOfficeIdException())
        {
        }

        public static implicit operator OfficeId(Guid id)
            => new(id);
    }
}
