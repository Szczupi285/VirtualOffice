using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ApplicationUser
{
    public class InvalidApplicationUserEmail : VirtualOfficeException
    {
        string Value;
        public InvalidApplicationUserEmail(string value) : base($"ApplicationUser E-Mail: {value} is invalid")
        {
            Value = value;
        }

    }
}
