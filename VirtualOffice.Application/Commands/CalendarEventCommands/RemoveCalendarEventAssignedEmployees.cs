using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record RemoveCalendarEventAssignedEmployees(Guid Id, HashSet<ApplicationUser> EmployeesToRemove) : IRequest;
}