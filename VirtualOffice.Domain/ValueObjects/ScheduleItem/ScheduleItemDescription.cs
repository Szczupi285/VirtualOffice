using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;

namespace VirtualOffice.Domain.ValueObjects.ScheduleItem
{
    public sealed record ScheduleItemDescription
    {
        string Value { get; }
        public ScheduleItemDescription(string value)
        {
            if (value is null)
                value = "";
            else if (value.Length > 1500)
                throw new TooLongScheduleItemDescriptionException(value, 1500);

            Value = value;
        }

        public static implicit operator ScheduleItemDescription(string content)
            => new(content);

        public static implicit operator string(ScheduleItemDescription content)
            => content.Value;
    }
}
