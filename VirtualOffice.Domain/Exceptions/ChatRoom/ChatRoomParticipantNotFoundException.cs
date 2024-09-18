using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class ChatRoomParticipantNotFoundException : VirtualOfficeException
    {
        private string Value;

        public ChatRoomParticipantNotFoundException(string value) : base($"Cannot find office member with the provided input: {value}.")
        {
            Value = value;
        }
    }
}