using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Note
{
    public class EmptyNoteTitleException : VirtualOfficeException
    {
        public EmptyNoteTitleException() : base("Note title cannot be empty")
        {
        }
    }
}
