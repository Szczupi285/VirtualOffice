using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class InvalidPrivateRoomParticipantsException : VirtualOfficeException
    {
        public InvalidPrivateRoomParticipantsException() : base($"Private Room must have exactly two participants")
        {
        }
    }
}