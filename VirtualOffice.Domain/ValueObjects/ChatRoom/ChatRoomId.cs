using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.Exceptions.ChatRoom;

namespace VirtualOffice.Domain.ValueObjects.AbstractChatRoom
{
    public sealed record ChatRoomId : AbstractRecordId
    {
        public ChatRoomId(Guid value) : base(value, new EmptyChatRoomIdExceptionException())
        {
        }

        public static implicit operator ChatRoomId(Guid id)
            => new(id);
    }
}
