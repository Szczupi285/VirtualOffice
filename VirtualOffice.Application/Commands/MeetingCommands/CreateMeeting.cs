using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record CreateMeeting(string Title, string Description, HashSet<ApplicationUser> AssignedEmployees, DateTime StartDate, DateTime EndDate) : IRequest;

}
