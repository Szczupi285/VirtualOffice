using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Event
{
    public class TooLongEventTitleException : VirtualOfficeException
    {
        string Value;
        public TooLongEventTitleException(string value, int length) : base($"Title: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}
