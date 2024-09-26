using MediatR;
using VirtualOffice.Application.DTO.EmployeeTask;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmployeeTasksForUser(Guid UserId) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;
}
