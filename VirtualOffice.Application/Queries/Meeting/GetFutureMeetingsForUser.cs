using MediatR;
using VirtualOffice.Application.DTO.Meeting;

namespace VirtualOffice.Application.Queries.Meeting
{
    public record GetFutureMeetingsForUser(Guid Id) : IRequest<IEnumerable<MeetingTitleDTO>>;
}
