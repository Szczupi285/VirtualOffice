using MediatR;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record DeleteEmployeeTask(Guid Guid) : IRequest;
    
}
