using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.Meeting
{
    public class MeetingDoesNotExistException : VirtualOfficeException
    {
        public MeetingDoesNotExistException(Guid guid) : base($"Meeting with id: {guid} does not exists")
        {
        }
    }
}
