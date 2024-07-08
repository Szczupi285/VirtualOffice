using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record CreateCalendarEvent(Guid Guid, string Title, string Description, HashSet<ApplicationUser> AssignedEmployees, DateTime StartDate, DateTime EndDate) : IRequest;


}
