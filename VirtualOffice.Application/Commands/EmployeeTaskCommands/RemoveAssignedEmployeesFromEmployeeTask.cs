using MediatR;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record RemoveAssignedEmployeesFromEmployeeTask(Guid Id, HashSet<Guid> EmployeesToRemove) : IRequest;
}