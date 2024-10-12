using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record AddCalendarEventAssignedEmployees(Guid Id, HashSet<Guid> EmployeesToAdd) : IRequest;
}