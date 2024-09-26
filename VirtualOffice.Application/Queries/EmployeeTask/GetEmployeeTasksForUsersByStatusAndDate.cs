using MediatR;
using VirtualOffice.Application.DTO.EmployeeTask;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmployeeTasksForUsersByStatusAndDate(Guid UserId, DateTime StartDate, DateTime EndDate, string Status) : IRequest<IEnumerable<EmployeeTaskTitleDTO>>;
}
