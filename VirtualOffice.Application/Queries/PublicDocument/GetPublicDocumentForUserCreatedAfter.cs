using MediatR;
using VirtualOffice.Application.DTO.PublicDocument;

namespace VirtualOffice.Application.Queries.PublicDocument
{
    public record GetPublicDocumentForUserCreatedAfter(Guid UserId, DateTime DateTime) : IRequest<IEnumerable<PublicDocumentTitleDTO>>;
}