using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Message
{
    public class EmptyMessageContentException : VirtualOfficeException
    {
        public EmptyMessageContentException() : base("Message Content cannot be empty")
        {
        }
    }
}