using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class UserIsNotAParticipantOfThisChatException : VirtualOfficeException
    {
        private Guid Id;

        public UserIsNotAParticipantOfThisChatException(Guid id) : base($"User with Id: {id} is not a participant of this chat")
        {
            Id = id;
        }
    }
}