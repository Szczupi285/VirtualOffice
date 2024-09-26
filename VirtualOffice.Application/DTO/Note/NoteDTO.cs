namespace VirtualOffice.Application.DTO.Note
{
    public class NoteDTO
    {
        public Guid Id { get; init; }
        public Guid _UserId { get; init; }
        public string _Title { get; init; }
        public string _Content { get; init; }

    }
}
