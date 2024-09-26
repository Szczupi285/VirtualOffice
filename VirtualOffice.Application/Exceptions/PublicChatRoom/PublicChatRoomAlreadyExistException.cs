using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.PublicChatRoom
{
    public class PublicChatRoomAlreadyExistException : VirtualOfficeException
    {
        public PublicChatRoomAlreadyExistException(Guid id) : base($"Public chat room with Id: {id} already exist")
        {
        }
    }
}
