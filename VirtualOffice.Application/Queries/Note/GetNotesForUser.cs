using MediatR;
using VirtualOffice.Application.DTO.Note;

namespace VirtualOffice.Application.Queries.Note
{
    public record GetNotesForUser(Guid UserId) : IRequest<IEnumerable<NoteTitleDTO>>;
}
