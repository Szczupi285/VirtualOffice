using MediatR;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record UpdateEmployeeTaskTitle(Guid Id, string Title) : IRequest;
}