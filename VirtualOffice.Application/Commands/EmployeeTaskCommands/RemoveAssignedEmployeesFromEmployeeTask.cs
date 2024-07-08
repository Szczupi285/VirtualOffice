using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record RemoveAssignedEmployeesToEmployeeTask(Guid Guid, HashSet<ApplicationUser> EmployeesToRemove) : IRequest;

}
