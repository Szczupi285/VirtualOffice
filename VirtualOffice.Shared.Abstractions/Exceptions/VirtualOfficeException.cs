using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Shared.Abstractions
{
    public abstract class VirtualOfficeException : Exception
    {
        protected VirtualOfficeException(string? message) : base(message)
        {
        }
    }
}
