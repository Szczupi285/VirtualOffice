using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Office;

namespace VirtualOffice.Domain.ValueObjects.Office
{
    public sealed record OfficeDescription
    {
        public string Value { get; }

        public OfficeDescription(string value)
        {

            if (value.Length > 200)
                throw new InvalidOfficeDescriptionException(value);
            else if(value is null)
                throw new OfficeDescriptionIsNullException();

            Value = value.Trim();
        }

        public static implicit operator string(OfficeDescription name)
            => name.Value;

        public static implicit operator OfficeDescription(string name)
            => new(name);
    }
}
}
