using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ApplicationUser
{
    public class InvalidApplicationUserNameException : VirtualOfficeException
    {
        string Value;
        public InvalidApplicationUserNameException(string value) 
            : base($"ApplicationUser name: {value} does not only contain letters or dots. There must be at least 1 letter and first character cannot be dot")
        {
            Value = value;
        }
    }
}
