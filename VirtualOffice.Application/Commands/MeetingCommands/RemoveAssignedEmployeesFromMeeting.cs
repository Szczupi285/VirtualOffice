using MediatR;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record RemoveAssignedEmployeesFromMeeting(Guid Id, HashSet<Guid> EmployeesToRemove) : IRequest;
}