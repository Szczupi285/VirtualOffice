using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.CalendarEventCommands
{
    public record CreateCalendarEvent(string Title, string Description, HashSet<ApplicationUser> AssignedEmployees, DateTime StartDate, DateTime EndDate) : IRequest;


}
