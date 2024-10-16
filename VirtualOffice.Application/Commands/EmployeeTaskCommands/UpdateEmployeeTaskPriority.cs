using MediatR;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record UpdateEmployeeTaskPriority(Guid Id, EmployeeTaskPriorityEnum Priority) : IRequest;
}