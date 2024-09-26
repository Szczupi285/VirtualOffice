using MediatR;
using VirtualOffice.Application.DTO.PublicDocument;

namespace VirtualOffice.Application.Queries.PublicDocument
{
    public record GetPublicDocumentsForUser(Guid UserId) : IRequest<IEnumerable<PublicDocumentTitleDTO>>;
}
