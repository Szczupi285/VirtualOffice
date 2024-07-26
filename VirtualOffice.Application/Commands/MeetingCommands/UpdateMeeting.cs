using MediatR;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record UpdateMeeting(Guid Guid, string Title, string Description, DateTime StartDate, DateTime EndDate) : IRequest;

}
