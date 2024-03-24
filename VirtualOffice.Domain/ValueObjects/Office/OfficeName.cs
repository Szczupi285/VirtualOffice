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
    public sealed record OfficeName
    {
        public string Value { get; }

        public OfficeName(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyOfficeNameException();
            else if (value.Length > 50)
                throw new InvalidOfficeNameException(value);

            Value = value.Trim();
        }

        public static implicit operator string(OfficeName name)
            => name.Value;

        public static implicit operator OfficeName(string name)
            => new(name);
    }
}
