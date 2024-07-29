using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.PrivateDocument;

namespace VirtualOffice.Application.Queries.PrivateDocument
{
    public record GetPrivateDocumentsForUserSortedByTitle(Guid UserId, string Title) : IRequest<IEnumerable<PrivateDocumentTitleDTO>>;
}
