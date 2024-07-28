using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.Meeting
{
    public record GetMeetingsForUserByDate(Guid userId, DateTime StartDate, DateTime EndDate) : IRequest<IEnumerable<MeetingDTO>>;
}
