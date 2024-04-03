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
            if (value is null)
                throw new OfficeDescriptionIsNullException();
            else if (value.Length > 200)
                throw new InvalidOfficeDescriptionException(value);
            

            Value = value.Trim();
        }

        public static implicit operator string(OfficeDescription name)
            => name.Value;

        public static implicit operator OfficeDescription(string name)
            => new(name);
    }
}

