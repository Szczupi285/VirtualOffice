using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.PublicDocument;

namespace VirtualOffice.Application.Queries.PublicDocument
{
    public record GetPublicDocumentsForUserSortedByTitle(Guid UserId, string Title) : IRequest<IEnumerable<PublicDocumentTitleDTO>>;
}
