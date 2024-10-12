using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record AddCalendarEventAssignedEmployees(Guid CalendarId, HashSet<Guid> EmployeesToAdd) : IRequest;
}