using MediatR;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record UpdateMeetingTitle(Guid Id, string Title) : IRequest;
}