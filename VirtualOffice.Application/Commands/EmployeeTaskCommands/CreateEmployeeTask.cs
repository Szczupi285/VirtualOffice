using MediatR;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record CreateEmployeeTask(string Title, string Description,
        HashSet<ApplicationUser> AssignedEmployees, DateTime StartDate, DateTime EndDate,
        EmployeeTaskPriorityEnum Priority) : IRequest;

}
