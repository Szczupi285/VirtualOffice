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
        public string _title { get; init; }
        public string _description { get; init; }
        public List<string> _attachmentFilePaths { get; init; }
        public DateTime _creationDate { get; init; }
    }
}
