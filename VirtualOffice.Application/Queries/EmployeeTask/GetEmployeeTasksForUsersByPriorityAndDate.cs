using MediatR;
using VirtualOffice.Application.DTO.EmployeeTask;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmployeeTasksForUsersByPriorityAndDate(Guid UserId, DateTime StartDate, DateTime EndDate, string Priority) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;

}
