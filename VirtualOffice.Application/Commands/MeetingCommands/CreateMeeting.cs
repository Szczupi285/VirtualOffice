using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record CreateMeeting(Guid Guid, string Title, string Description, HashSet<ApplicationUser> AssignedEmployees, DateTime StartDate, DateTime EndDate) : IRequest;
    
}
