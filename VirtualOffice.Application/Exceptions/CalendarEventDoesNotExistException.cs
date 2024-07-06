using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions
{
    public class CalendarEventDoesNotExistException : VirtualOfficeException
    {
        public CalendarEventDoesNotExistException(Guid guid) : base($"Calendar event with id: {guid} doesn't exists")
        {
        }
    }
}
