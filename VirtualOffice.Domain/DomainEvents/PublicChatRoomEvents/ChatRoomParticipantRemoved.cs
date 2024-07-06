using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.PublicChatRoomEvents
{
    public record ChatRoomParticipantRemoved(PublicChatRoom room, ApplicationUser participant) : IDomainEvent;
    
}
