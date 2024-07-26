using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record AddAssignedEmployeesToEmployeeTask(Guid Guid, HashSet<ApplicationUser> EmployeesToAdd) : IRequest;

}
