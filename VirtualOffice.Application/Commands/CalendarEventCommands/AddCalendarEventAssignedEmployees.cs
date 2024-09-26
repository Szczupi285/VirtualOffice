using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record AddCalendarEventAssignedEmployees(Guid Id, HashSet<ApplicationUser> EmployeesToAdd) : IRequest;
}