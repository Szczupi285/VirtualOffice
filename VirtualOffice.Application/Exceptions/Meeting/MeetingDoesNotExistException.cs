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
