using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.Meeting;

namespace VirtualOffice.Application.Queries.Meeting
{
    public record GetMeetingById(Guid MeetingId) : IRequest<MeetingDTO>;
}
