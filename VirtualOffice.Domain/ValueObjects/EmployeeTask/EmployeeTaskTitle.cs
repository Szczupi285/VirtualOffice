using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.EmployeeTask;

namespace VirtualOffice.Domain.ValueObjects.EmployeeTask
{
    public sealed record EmployeeTaskTitle : AbstractRecordName
    {
        public EmployeeTaskTitle(string value) : base(value, 100, new EmptyEmployeeTaskTitleException(), new TooLongEmployeeTaskTitleException(value, 100))
        {
        }

        public static implicit operator EmployeeTaskTitle(string content)
            => new(content);
    }
}
