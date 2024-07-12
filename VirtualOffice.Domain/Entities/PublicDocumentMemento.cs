﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Entities
{
    public class PublicDocumentMemento
    {
        public Guid Id { get; }
        public string _title { get; }
        public string _content { get; }
        public ICollection<DocumentFilePath>? _attachmentFilePaths { get; }
        public ValueTuple<DocumentCreationDate, ApplicationUserId> _creationDetails { get; }

        public ICollection<ApplicationUserId> _eligibleForRead { get; }

        public ICollection<ApplicationUserId> _eligibleForWrite { get; }


       

        public PublicDocumentMemento(Guid id, string title, string content, ICollection<DocumentFilePath>? attachmentFilePaths,
            (DocumentCreationDate, ApplicationUserId) creationDetails, ICollection<ApplicationUserId> eligibleForRead, ICollection<ApplicationUserId> eligibleForWrite)
        {
            Id = id;
            _title = title;
            _content = content;
            _attachmentFilePaths = attachmentFilePaths;
            _creationDetails = creationDetails;
            _eligibleForRead = eligibleForRead;
            _eligibleForWrite = eligibleForWrite;
        }
    }
}
