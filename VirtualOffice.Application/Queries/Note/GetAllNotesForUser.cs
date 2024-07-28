﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.Note
{
    public record GetAllNotesForUser(Guid UserId) : IRequest<IEnumerable<NoteDTO>>;
}
