using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class EmptyChatRoomIdException : VirtualOfficeException
    {
        public EmptyChatRoomIdException()
            : base("ChatRoom Id cannot be empty")
        {
        }
    }
}