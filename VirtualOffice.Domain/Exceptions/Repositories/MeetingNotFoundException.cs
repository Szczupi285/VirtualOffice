using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Repositories
{
    public class MeetingNotFoundException : VirtualOfficeException
    {
        public MeetingNotFoundException(Guid guid) : base($"Meeting with Id: {guid} has not been found")
        {
        }
    }
}