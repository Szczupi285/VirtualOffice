namespace VirtualOffice.Domain.DomainEvents.EmployeeTask
{
    public record EmployeeTaskDisabled(Guid Id) : IDomainEvent;
}