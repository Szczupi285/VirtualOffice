using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.Meeting
{
    public record GetFutureMeetingsForUser(Guid Id) : IRequest<IEnumerable<MeetingTitleDTO>>;
}
