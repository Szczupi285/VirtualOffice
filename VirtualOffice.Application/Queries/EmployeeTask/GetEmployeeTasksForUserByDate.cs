using MediatR;
using VirtualOffice.Application.DTO.EmployeeTask;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmployeeTasksForUserByDate(Guid UserId, DateTime StartDate, DateTime EndDate) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;
}
