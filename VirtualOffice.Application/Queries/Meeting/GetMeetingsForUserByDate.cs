using MediatR;
using VirtualOffice.Application.DTO.Meeting;

namespace VirtualOffice.Application.Queries.Meeting
{
    public record GetMeetingsForUserByDate(Guid userId, DateTime StartDate, DateTime EndDate) : IRequest<IEnumerable<MeetingTitleDTO>>;
}
