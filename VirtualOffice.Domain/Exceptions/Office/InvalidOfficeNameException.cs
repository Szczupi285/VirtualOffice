using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Office
{
    public class InvalidOfficeNameException : VirtualOfficeException
    {
        string Value;
        public InvalidOfficeNameException(string value) : base($"Office name: {value} is more than 50 characters long")
        {
            Value = value;  
        }
    }
}
