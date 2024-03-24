using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Office
{
    public class InvalidOfficeDescriptionException : VirtualOfficeException
    {
        string Value;
        public InvalidOfficeDescriptionException(string value) : base($"Description: {value} is more than 200 characters long")
        {
        }
    }
}
