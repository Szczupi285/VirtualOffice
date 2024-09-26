using MediatR;
using VirtualOffice.Application.DTO.Meeting;

namespace VirtualOffice.Application.Queries.Meeting
{
    public record GetMeetingsForUser(Guid UserId) : IRequest<IEnumerable<MeetingTitleDTO>>;
}
