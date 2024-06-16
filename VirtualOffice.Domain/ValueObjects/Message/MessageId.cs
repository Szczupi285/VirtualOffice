using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Message;

namespace VirtualOffice.Domain.ValueObjects.Message
{

    public sealed record MessageId : AbstractRecordId
    {
        public MessageId(Guid value) : base(value, new EmptyMessageIdException())
        {
        }

        public static implicit operator MessageId(Guid id)
            => new(id);
    }
}
