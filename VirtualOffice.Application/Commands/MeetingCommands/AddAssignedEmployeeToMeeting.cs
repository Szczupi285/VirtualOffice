using MediatR;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record AddAssignedEmployeesToMeeting(Guid Id, HashSet<Guid> EmployeesToAdd) : IRequest;
}