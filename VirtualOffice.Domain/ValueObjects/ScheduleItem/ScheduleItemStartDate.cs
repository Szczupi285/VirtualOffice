using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Domain.ValueObjects.ScheduleItem
{
    public sealed record ScheduleItemStartDate
    {
        public DateTime Value { get; }

        public ScheduleItemStartDate(DateTime value)
        {
            // since assigning value to EmployeeTaskStartDate is not fully instant we decrease minutes 
            // so it won't return exception if we try assing datetime.UtcNow
            if (value < DateTime.UtcNow.AddMinutes(-1))
                throw new ScheduleItemStartDateCannotBePastException(value);

            Value = value;
        }

        public static implicit operator DateTime(ScheduleItemStartDate startDate)
            => startDate.Value;

        public static implicit operator ScheduleItemStartDate(DateTime startDate)
            => new(startDate);

        public static bool operator ==(ScheduleItemStartDate startDate, DateTime value) => startDate.Value == value;
        public static bool operator != (ScheduleItemStartDate startDate, DateTime value) => startDate.Value != value;

    }
}
