using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.PublicDocument;

namespace VirtualOffice.Application.Queries.PublicDocument
{
    public record GetPublicDocumentsForUser(Guid UserId) : IRequest<IEnumerable<PublicDocumentTitleDTO>>;
}
