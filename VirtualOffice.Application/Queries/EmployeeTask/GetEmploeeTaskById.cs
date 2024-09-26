using MediatR;
using VirtualOffice.Application.DTO.EmployeeTask;

namespace VirtualOffice.Application.Queries.EmployeeTask
{
    public record GetEmploeeTaskById(Guid EmployeeTaskId) : IRequest<EmployeeTaskDTO>;
}
