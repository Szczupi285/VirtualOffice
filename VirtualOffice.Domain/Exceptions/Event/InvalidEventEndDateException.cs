using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Event
{
    public class InvalidEventEndDateException : VirtualOfficeException
    {
        DateTime Value;
        public InvalidEventEndDateException(DateTime value) : base($"Event EndDate cannot be in the past")
        {
            Value = value;
        }
    }
}
