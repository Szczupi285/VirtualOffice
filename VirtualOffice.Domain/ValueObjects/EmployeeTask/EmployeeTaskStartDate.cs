using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Exceptions.EmployeeTask;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;

namespace VirtualOffice.Domain.ValueObjects.EmployeeTask
{
    public sealed record EmployeeTaskStartDate
    {
        public DateTime Value { get; }

        public EmployeeTaskStartDate(DateTime value)
        {
            // since assigning value to EmployeeTaskStartDate is not fully instant we decrease minutes 
            // so it won't return exception if we try assing datetime.UtcNow
            if (value < DateTime.UtcNow.AddMinutes(-1))
                throw new EmployeeTaskStartDateCannotBePastException(value);

            Value = value;
        }

        public static implicit operator DateTime(EmployeeTaskStartDate startDate)
            => startDate.Value;

        public static implicit operator EmployeeTaskStartDate(DateTime startDate)
            => new(startDate);
    }
}
