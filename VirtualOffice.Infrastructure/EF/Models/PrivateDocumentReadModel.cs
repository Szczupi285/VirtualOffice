﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.Interfaces;

namespace VirtualOffice.Infrastructure.EF.Models
{
    public class PrivateDocumentReadModel : EntityId
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string> AttachmentFilePaths { get; set; }
    }
}