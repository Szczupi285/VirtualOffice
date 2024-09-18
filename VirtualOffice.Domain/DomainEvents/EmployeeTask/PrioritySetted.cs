using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Domain.DomainEvents.EmployeeTask
{
    public record PrioritySetted(AbstractScheduleItem abstractScheduleItem, EmployeeTaskPriorityEnum priorityEnum) : IDomainEvent;
}