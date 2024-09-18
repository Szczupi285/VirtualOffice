using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Infrastructure.EF.Models
{
    public class PublicDocumentReadModel
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