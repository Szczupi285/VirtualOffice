using MediatR;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record UpdateEmployeeTask(Guid Id, string Title, string Description, 
        DateTime EndDate, EmployeeTaskStatusEnum Status, EmployeeTaskPriorityEnum Priority) : IRequest;

}
