using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Application.DTO.PrivateDocument
{
    public class PrivateDocumentDTO
    {
        public Guid Id { get; init; }
        public string _Title { get; init; }
        public string _Description { get; init; }
        public List<string> _AttachmentFilePaths { get; init; }
        public DateTime _CreationDate { get; init; }
    }
}
