using MediatR;

namespace VirtualOffice.Application.Commands.EmployeeTaskCommands
{
    public record RescheduleEmployeeTask(Guid Id, DateTime StartDate, DateTime EndDate) : IRequest;
}
