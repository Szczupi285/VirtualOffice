using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Event;

namespace VirtualOffice.Domain.ValueObjects.Event
{
    public sealed record EventDescription : AbstractRecordName
    {
        public EventDescription(string value) : base(value, 100, new EmptyEventDescriptionException(), new TooLongEventDescriptionException(value, 100))
        {
        }

        public static implicit operator EventDescription(string content)
            => new(content);
    }
}
