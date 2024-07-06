using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.ValueObjects.ScheduleItem
{
    public record ScheduleItemEndDate
    {
        public DateTime Value { get; }

        public ScheduleItemEndDate(DateTime value)
        {
            if (value <= DateTime.UtcNow)
                throw new InvalidScheduleItemEndDateException(value);
            Value = value;
        }

        public static implicit operator DateTime(ScheduleItemEndDate EndDate)
            => EndDate.Value;

        public static implicit operator ScheduleItemEndDate(DateTime EndDate)
            => new(EndDate);
    }
}

