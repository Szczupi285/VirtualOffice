using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.EmployeeTask;

namespace VirtualOffice.Domain.ValueObjects.EmployeeTask
{
    public sealed record EmployeeTaskEndDate
    {
        public DateTime Value { get; }

        public EmployeeTaskEndDate(DateTime value)
        {
            if (value < DateTime.UtcNow)
                throw new InvalidEmployeeTaskEndDateException(value);
            Value = value;
        }

        public static implicit operator DateTime(EmployeeTaskEndDate EndDate)
            => EndDate.Value;

        public static implicit operator EmployeeTaskEndDate(DateTime EndDate)
            => new(EndDate);
    }
}

