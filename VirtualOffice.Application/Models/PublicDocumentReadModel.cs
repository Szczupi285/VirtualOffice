using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.Models
{
    public class PublicDocumentReadModel : EntityId
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string> AttachmentFilePaths { get; set; }
        public CreationDetailsReadModel CreationDetails { get; set; }
        public List<string> EligibleForReadIds { get; set; }
        public List<string> EligibleForWriteIds { get; set; }
    }
}