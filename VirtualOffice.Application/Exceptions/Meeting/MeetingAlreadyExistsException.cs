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
