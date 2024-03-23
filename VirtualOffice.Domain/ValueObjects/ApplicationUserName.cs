using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions;

namespace VirtualOffice.Domain.ValueObjects
{
    public record ApplicationUserName
    {
        public string Value { get; }

        public ApplicationUserName(string value)
        {

            if (string.IsNullOrWhiteSpace(value)) 
            {
                throw new EmptyApplicationUserNameException();
            }

            Value = value;
        }

        public static implicit operator string(ApplicationUserName name)
            => name.Value;

        public static implicit operator ApplicationUserName(string name)
            => new(name);

    }
}
