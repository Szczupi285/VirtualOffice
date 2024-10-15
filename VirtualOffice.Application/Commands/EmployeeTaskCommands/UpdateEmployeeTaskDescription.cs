using MediatR;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record UpdateEmployeeTaskDescription(Guid Id, string Description) : IRequest;
}