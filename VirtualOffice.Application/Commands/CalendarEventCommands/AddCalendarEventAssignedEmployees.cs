using VirtualOffice.Domain.Entities;
using MediatR;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record AddCalendarEventAssignedEmployees(Guid Guid, HashSet<ApplicationUser> EmployeesToAdd) : IRequest;

}
