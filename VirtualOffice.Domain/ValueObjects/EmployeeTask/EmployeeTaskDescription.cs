using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.EmployeeTask;

namespace VirtualOffice.Domain.ValueObjects.EmployeeTask
{
    public sealed record EmployeeTaskDescription : AbstractRecordName
    {
        public EmployeeTaskDescription(string value) : base(value, 1500, new EmptyEmployeeTaskDescriptionException(), new TooLongEmployeeTaskDescriptionException(value, 1500))
        {
        }

        public static implicit operator EmployeeTaskDescription(string content)
            => new(content);
    }
}
