using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoomService
{
    public class ChatRoomIdNotFoundException : VirtualOfficeException
    {
        private Guid Id;

        public ChatRoomIdNotFoundException(Guid id) : base($"Chat Room with Id: {id} Has not been found")
        {
            Id = id;
        }
    }
}