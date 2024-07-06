using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.ScheduleItem
{
    public record EmployeeAddedToScheduleItem(AbstractScheduleItem abstractScheduleItem, ApplicationUser user) : IDomainEvent;
    
}
