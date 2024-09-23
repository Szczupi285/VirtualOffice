using MediatR;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record UpdateMeeting(Guid Id, string Title, string Description, DateTime StartDate, DateTime EndDate) : IRequest;
}