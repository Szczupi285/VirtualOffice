using MediatR;
using VirtualOffice.Application.DTO.EmployeeTask;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmployeeTasksForUsersByStatus(Guid UserId, string Status) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;
}
