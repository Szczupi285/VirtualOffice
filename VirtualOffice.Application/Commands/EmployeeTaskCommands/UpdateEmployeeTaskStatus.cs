using MediatR;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record UpdateEmployeeTaskStatus(Guid Id, EmployeeTaskStatusEnum Status) : IRequest;
}