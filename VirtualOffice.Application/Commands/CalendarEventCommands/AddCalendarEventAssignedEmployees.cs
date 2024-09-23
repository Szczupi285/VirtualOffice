using VirtualOffice.Domain.Entities;
using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record AddCalendarEventAssignedEmployees(Guid Id, HashSet<ApplicationUser> EmployeesToAdd) : IRequest;
}