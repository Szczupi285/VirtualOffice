using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class TooLongPublicChatRoomNameException : VirtualOfficeException
    {
        private string Value;

        public TooLongPublicChatRoomNameException(string value, ushort length) : base($"PublicChatRoomName: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}