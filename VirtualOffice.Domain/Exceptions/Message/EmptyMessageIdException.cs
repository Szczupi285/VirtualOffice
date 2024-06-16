using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Message
{
    public class EmptyMessageIdException : VirtualOfficeException
    {
        public EmptyMessageIdException()
           : base("Message Id cannot be empty")
        {
        }
    }
}
