using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.PrivateChatRoom
{
    public class PrivateChatRoomDoesNotExistException : VirtualOfficeException
    {
        public PrivateChatRoomDoesNotExistException(Guid Id) : base($"Chat room with {Id} does not exist")
        {
        }
    }
}
