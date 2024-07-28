﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO;

namespace VirtualOffice.Application.Queries.PrivateDocument
{
    public record GetPrivateDocumentForUserSortedByTitle(Guid UserId, string Title) : IRequest<IEnumerable<PrivateDocumentDTO>>;
}
