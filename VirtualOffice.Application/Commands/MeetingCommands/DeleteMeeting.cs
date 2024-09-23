using MediatR;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record DeleteMeeting(Guid Id) : IRequest;
}