using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Event
{
    public class EmptyEventTitleException : VirtualOfficeException
    {
        public EmptyEventTitleException() : base("Event title cannot be empty")
        {
        }
    }
}
