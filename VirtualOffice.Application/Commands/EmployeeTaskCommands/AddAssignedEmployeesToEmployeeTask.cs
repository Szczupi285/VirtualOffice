using MediatR;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record AddAssignedEmployeesToEmployeeTask(Guid Id, HashSet<Guid> EmployeesToAdd) : IRequest;
}