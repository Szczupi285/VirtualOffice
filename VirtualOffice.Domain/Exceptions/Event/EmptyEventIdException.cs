using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Event
{
    public class EmptyEventIdException : VirtualOfficeException
    {
        public EmptyEventIdException()
           : base("Event Id cannot be empty")
        {
        }
    }
}
