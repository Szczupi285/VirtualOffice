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
        private static bool DateTimeEquals(DateTime dt1, DateTime dt2)
        {
            // Truncate milliseconds for both DateTime objects
            DateTime dt1Truncated = dt1.AddTicks(-(dt1.Ticks % TimeSpan.TicksPerSecond));
            DateTime dt2Truncated = dt2.AddTicks(-(dt2.Ticks % TimeSpan.TicksPerSecond));

            return dt1Truncated == dt2Truncated;
        }

        public static implicit operator DateTime(ScheduleItemEndDate EndDate)
            => EndDate.Value;

        public static implicit operator ScheduleItemEndDate(DateTime EndDate)
            => new(EndDate);

        public static bool operator ==(ScheduleItemEndDate startDate, DateTime value) => DateTimeEquals(startDate.Value, value);
        public static bool operator !=(ScheduleItemEndDate startDate, DateTime value) => !DateTimeEquals(startDate.Value, value);
    }
}

