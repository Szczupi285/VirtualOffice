using MediatR;
using VirtualOffice.Application.DTO.Meeting;

namespace VirtualOffice.Application.Queries.Meeting
{
    public record GetMeetingById(Guid MeetingId) : IRequest<MeetingDTO>;
}
