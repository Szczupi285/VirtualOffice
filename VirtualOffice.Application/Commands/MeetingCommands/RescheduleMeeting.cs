using MediatR;

namespace VirtualOffice.Application.Commands.MeetingCommands
{
    public record RescheduleMeeting(Guid Id, DateTime StartDate, DateTime EndDate) : IRequest;
}