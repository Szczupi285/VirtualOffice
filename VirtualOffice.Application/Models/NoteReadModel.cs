using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.Models
{
    public class NoteReadModel : EntityId
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public EmployeeReadModel CreatedBy { get; set; }
    }
}