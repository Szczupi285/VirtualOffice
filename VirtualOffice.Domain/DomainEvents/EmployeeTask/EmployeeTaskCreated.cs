using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.EmployeeTask
{
    public record EmployeeTaskCreated(Guid Id, string Title, string Description,
        HashSet<ApplicationUser> AssignedEmployees, DateTime StartDate, DateTime EndDate,
        EmployeeTaskPriorityEnum Priority, EmployeeTaskStatusEnum Status) : IDomainEvent;
}