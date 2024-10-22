namespace VirtualOffice.Application.DTO.Note
{
    public class NoteDTO
    {
        public string Title { get; init; }
        public string Content { get; init; }
        public Guid UserId { get; init; }
    }
}