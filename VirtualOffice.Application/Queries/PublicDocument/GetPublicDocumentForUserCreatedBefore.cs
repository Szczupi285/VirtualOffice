using MediatR;
using VirtualOffice.Application.DTO.PublicDocument;

namespace VirtualOffice.Application.Queries.PublicDocument
{
    public record GetPublicDocumentForUserCreatedBefore(Guid UserId, DateTime DateTime) : IRequest<IEnumerable<PublicDocumentTitleDTO>>;
}
