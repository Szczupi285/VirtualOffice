using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.PrivateChatRoom
{
    public class PrivateChatRoomAlreadyExistException : VirtualOfficeException
    {
        public PrivateChatRoomAlreadyExistException(Guid id) : base($"Private chat room with Id: {id} already exist")
        {
        }
    }
}
