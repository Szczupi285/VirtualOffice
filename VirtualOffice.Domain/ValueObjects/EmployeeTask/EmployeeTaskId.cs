using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;

namespace VirtualOffice.Domain.ValueObjects.EmployeeTask
{
    public sealed record EmployeeTaskId : AbstractRecordId
    {
        public EmployeeTaskId(Guid value) : base(value, new EmptyEmployeeTaskIdException())
        {
        }

        public static implicit operator EmployeeTaskId(Guid id)
            => new(id);
    }
}
