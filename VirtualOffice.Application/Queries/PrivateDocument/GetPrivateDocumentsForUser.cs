using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.PrivateDocument;

namespace VirtualOffice.Application.Queries.PrivateDocument
{
    public record GetPrivateDocumentsForUser(Guid UserId) : IRequest<IEnumerable<PrivateDocumentTitleDTO>>;
}
