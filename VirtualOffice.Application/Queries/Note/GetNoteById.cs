using MediatR;
using VirtualOffice.Application.DTO.Note;

namespace VirtualOffice.Application.Queries.Note
{
    public record GetNoteById(Guid NoteId) : IRequest<NoteDTO>;
}
