using MediatR;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record UpdateMeetingDescription(Guid Id, string Description) : IRequest;
}