using VIrtualOffice.Domain.Exceptions.ScheduleItem;

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
        private static bool DateTimeEquals(DateTime dt1, DateTime dt2)
        {
            // Truncate milliseconds for both DateTime objects
            DateTime dt1Truncated = dt1.AddTicks(-(dt1.Ticks % TimeSpan.TicksPerSecond));
            DateTime dt2Truncated = dt2.AddTicks(-(dt2.Ticks % TimeSpan.TicksPerSecond));

            return dt1Truncated == dt2Truncated;
        }

        public static implicit operator DateTime(ScheduleItemStartDate startDate)
            => startDate.Value;

        public static implicit operator ScheduleItemStartDate(DateTime startDate)
            => new(startDate);

        public static bool operator ==(ScheduleItemStartDate startDate, DateTime value) => DateTimeEquals(startDate.Value, value);
        public static bool operator !=(ScheduleItemStartDate startDate, DateTime value) => !DateTimeEquals(startDate.Value, value);
    }
}