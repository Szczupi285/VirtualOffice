using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record AddAssignedEmployeesToEmployeeTask(Guid Id, HashSet<ApplicationUser> EmployeesToAdd) : IRequest;
}