using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Event
{
    public class TooLongEventDescriptionException : VirtualOfficeException
    {
        string Value;
        public TooLongEventDescriptionException(string value, int length) : base($"Description: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}
