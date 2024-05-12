using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.Event;

namespace VirtualOffice.Domain.ValueObjects.Event
{
    public sealed record EventEndDate
    {
        public DateTime Value { get; }

        public EventEndDate(DateTime value)
        {
            if (value <= DateTime.UtcNow)
                throw new InvalidEventEndDateException(value);
            Value = value;
        }

        public static implicit operator DateTime(EventEndDate EndDate)
            => EndDate.Value;

        public static implicit operator EventEndDate(DateTime EndDate)
            => new(EndDate);
    }
}
