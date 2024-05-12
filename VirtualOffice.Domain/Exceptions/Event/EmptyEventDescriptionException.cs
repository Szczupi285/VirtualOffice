using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Event
{
    public class EmptyEventDescriptionException : VirtualOfficeException
    {
        public EmptyEventDescriptionException() : base("Event Description cannot be empty")
        {
        }
    }
}
