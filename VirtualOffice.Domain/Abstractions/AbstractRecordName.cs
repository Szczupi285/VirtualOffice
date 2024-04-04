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
    public abstract record AbstractRecordName
    {
        public string Value { get; }

        public AbstractRecordName(string value, int Length, params VirtualOfficeException[] virtOfficeEx)
        {

            if (string.IsNullOrWhiteSpace(value))
                throw virtOfficeEx[0];
            else if (value.Length > Length)
                throw virtOfficeEx[1];

            Value = value.Trim();
        }

        
        public static implicit operator string(AbstractRecordName name)
            => name.Value;
        // since we can't create new instances in abstract class we have to make implicit conversion in derived record

    }
}
