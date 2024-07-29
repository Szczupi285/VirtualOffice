using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.PrivateDocument;
using VirtualOffice.Application.DTO.PublicDocument;

namespace VirtualOffice.Application.Queries.PublicDocument
{
    public record GerPublicDocumentForUserCreatedAfter(Guid UserId, DateTime DateTime) : IRequest<IEnumerable<PublicDocumentTitleDTO>>;
}
