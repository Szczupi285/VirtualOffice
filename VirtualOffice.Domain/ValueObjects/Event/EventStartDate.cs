using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Event;
using VirtualOffice.Domain.ValueObjects.Event;

namespace VirtualOffice.Domain.ValueObjects.Event
{
    public sealed record EventStartDate
    {
        public DateTime Value { get; }

        public EventStartDate(DateTime value)
        {
            // since assigning value to EventStartDate is not fully instant we decrease minutes 
            // so it won't return exception if we try assing datetime.UtcNow
            if (value < DateTime.UtcNow.AddMinutes(-1))
                throw new EventStartDateCannotBePastException(value);

            Value = value;
        }

        public static implicit operator DateTime(EventStartDate startDate)
            => startDate.Value;

        public static implicit operator EventStartDate(DateTime startDate)
            => new(startDate);
    }
}
