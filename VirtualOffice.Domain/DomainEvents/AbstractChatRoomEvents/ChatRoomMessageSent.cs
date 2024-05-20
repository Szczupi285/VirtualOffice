using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.AbstractChatRoomEvents
{
    public record ChatRoomMessageSent(AbstractChatRoom room, Message message) : IDomainEvent
    
}
