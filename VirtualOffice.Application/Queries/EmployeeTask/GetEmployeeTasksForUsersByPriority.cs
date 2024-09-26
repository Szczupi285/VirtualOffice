using MediatR;
using VirtualOffice.Application.DTO.EmployeeTask;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmployeeTasksForUsersByPriority(Guid Id, string Priority) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;
}
