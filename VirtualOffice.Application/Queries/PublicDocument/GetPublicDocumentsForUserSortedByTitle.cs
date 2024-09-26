using MediatR;
using VirtualOffice.Application.DTO.PublicDocument;

namespace VirtualOffice.Application.Queries.PublicDocument
{
    public record GetPublicDocumentsForUserSortedByTitle(Guid UserId, string Title) : IRequest<IEnumerable<PublicDocumentTitleDTO>>;
}
