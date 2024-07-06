using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.Meeting
{
    public class MeetingAlreadyExistsException : VirtualOfficeException
    {
        public MeetingAlreadyExistsException(Guid guid) : base($"Meeting with id: {guid} already exists")
        {
        }
    }
}
