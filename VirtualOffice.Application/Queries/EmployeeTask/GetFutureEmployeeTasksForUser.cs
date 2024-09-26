using MediatR;
using VirtualOffice.Application.DTO.EmployeeTask;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetFutureEmployeeTasksForUser(Guid UserId) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;
}
