using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;
using VirtualOffice.Domain.Exceptions.Event;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.ValueObjects.Event
{
    public sealed record EventTitle : AbstractRecordName
    {
        public EventTitle(string value) : base(value, 100, new EmptyEventTitleException(), new TooLongEventTitleException(value, 100))
        {
        }

        public static implicit operator EventTitle(string content)
            => new(content);
    }
}
