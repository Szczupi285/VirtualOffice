using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;
using VirtualOffice.Domain.Exceptions.Event;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;

namespace VirtualOffice.Domain.ValueObjects.Event
{
    public sealed record EventId : AbstractRecordId
    {
        public EventId(Guid value) : base(value, new EmptyEventIdException())
        {
        }

        public static implicit operator EventId(Guid id)
            => new(id);
    }
}
