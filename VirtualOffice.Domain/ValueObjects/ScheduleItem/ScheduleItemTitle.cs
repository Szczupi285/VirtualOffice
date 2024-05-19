using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.ValueObjects.EmployeeTask
{
    public sealed record ScheduleItemTitle : AbstractRecordName
    {
        public ScheduleItemTitle(string value) : base(value, 100, new EmptyScheduleItemTitleException(), new TooLongScheduleItemTitleException(value, 100))
        {
        }

        public static implicit operator ScheduleItemTitle(string content)
            => new(content);
    }
}
