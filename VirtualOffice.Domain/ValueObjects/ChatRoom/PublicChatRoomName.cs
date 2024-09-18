using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ChatRoom;

namespace VirtualOffice.Domain.ValueObjects.ChatRoom
{
    public sealed record PublicChatRoomName : AbstractRecordName
    {
        public PublicChatRoomName(string value) : base(value, 60, new EmptyPublicChatRoomNameException(), new TooLongPublicChatRoomNameException(value, 60))
        {
        }

        public static implicit operator PublicChatRoomName(string title)
            => new(title);
    }
}