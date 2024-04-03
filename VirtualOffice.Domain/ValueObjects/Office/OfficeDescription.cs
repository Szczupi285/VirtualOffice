using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Office;

namespace VirtualOffice.Domain.ValueObjects.Office
{
    public sealed record OfficeDescription : AbstractRecordName
    {
        public OfficeDescription(string value) : base(value, 200, new OfficeDescriptionIsNullException(), new InvalidOfficeDescriptionException(value))
        {
        }
        // since we can't create new instances in abstract class we have to make implicit conversion here
        public static implicit operator OfficeDescription(string name)
            => new(name);
    }
}

