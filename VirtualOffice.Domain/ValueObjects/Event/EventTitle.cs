using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.EmployeeTask;
using VirtualOffice.Domain.Exceptions.Event;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;

namespace VirtualOffice.Domain.ValueObjects.Event
{
    public sealed record EventTitle : AbstractRecordName
    {
        public EventTitle(string value) : base(value, 100, new EmptyEventTitleException(), new TooLongEventTitleException(value, 100))
        {
        }

        public static implicit operator EventTitle(string content)
            => new(content);
    }
}
