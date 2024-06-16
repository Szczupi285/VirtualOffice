using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Domain.DomainEvents.EmployeeTask
{
    public record StatusUpdated(AbstractScheduleItem abstractScheduleItem, EmployeeTaskStatusEnum statusEnum) : IDomainEvent;
}
