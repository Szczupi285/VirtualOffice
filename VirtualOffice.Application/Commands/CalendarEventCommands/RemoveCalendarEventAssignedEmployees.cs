using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record RemoveCalendarEventAssignedEmployees(Guid CalendarId, HashSet<Guid> EmployeesToRemove) : IRequest;
}