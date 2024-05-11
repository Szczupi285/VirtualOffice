using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.EmployeeTask;

namespace VirtualOffice.Domain.ValueObjects.EmployeeTask
{
    public sealed record EmployeeTaskDescription
    {
        string Value { get; }
        public EmployeeTaskDescription(string value)
        {
            if (value is null)
                value = "";
            else if (value.Length > 1500)
                throw new TooLongEmployeeTaskDescriptionException(value, 1500);

            Value = value;
        }

        public static implicit operator EmployeeTaskDescription(string content)
            => new(content);

        public static implicit operator string(EmployeeTaskDescription content)
            => content.Value;
    }
}
