using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ChatRoom
{
    public class UserIsNotAParticipantOfThisChat : VirtualOfficeException
    {
        Guid Id;
        public UserIsNotAParticipantOfThisChat(Guid id) : base($"User with Id: {id} is not a participant of this chat")
        {
            Id = id;
        }
    }
}
