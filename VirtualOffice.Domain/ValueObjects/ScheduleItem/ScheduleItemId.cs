using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.ValueObjects.ScheduleItem
{
    public sealed record ScheduleItemId : AbstractRecordId
    {
        public ScheduleItemId(Guid value) : base(value, new EmptyEmployeeScheduleItemIdException())
        {
        }

        public static implicit operator ScheduleItemId(Guid id)
            => new(id);
    }
}
