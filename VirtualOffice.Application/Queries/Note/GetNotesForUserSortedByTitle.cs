using MediatR;
using VirtualOffice.Application.DTO.Note;

namespace VirtualOffice.Application.Queries.Note
{
    public record GetNotesForUserSortedByTitle(Guid UserId, string Title) : IRequest<IEnumerable<NoteTitleDTO>>;
}
