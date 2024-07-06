using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Message
{
    public class TooLongMessageContentException : VirtualOfficeException
    {
        string Value;
        public TooLongMessageContentException(string value, ushort length) : base($"Message Content: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}
