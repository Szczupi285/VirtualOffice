using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.Note;

namespace VirtualOffice.Application.Queries.Note
{
    public record GetNotesForUser(Guid UserId) : IRequest<IEnumerable<NoteTitleDTO>>;
}
