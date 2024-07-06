﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.DomainEvents.AbstractDocumentEvents;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AbstractDocument : AggregateRoot<DocumentId>
    {
        // created with builder.

        public DocumentTitle _title { get; private protected set; }

        public DocumentContent _content { get; private protected set; }

        public AbstractDocument? _previousVersion { get; private protected set; }

        public ICollection<DocumentFilePath>? _attachmentFilePaths { get; private protected set; }

        internal void AddId(Guid id) => Id = id;

        internal void AddTitle(string title) => _title = title;

        internal void AddContent(string content) => _content = content;

        internal void AddPreviousVersion(AbstractDocument previousVersion) => _previousVersion = previousVersion;

        internal void AddAttachment(ICollection<DocumentFilePath> attachmentFilePaths) => _attachmentFilePaths = attachmentFilePaths;

        public void SetTitle(DocumentTitle title)
        {
            _title = title;
            AddEvent(new DocumentTitleSetted(this, title));
        }

        public void SetContent(DocumentContent content)
        {
            _content = content;
            AddEvent(new DocumentContentSetted(this, content));
        }
        public void SetPreviousVersion(AbstractDocument previousVersion)
        {
            _previousVersion = previousVersion;
            AddEvent(new DocumentPreviousVersionSetted(this, previousVersion));
        }
        public void AddNewAttachment(DocumentFilePath attachmentFilePath)
        {
            if(_attachmentFilePaths is not null)
            {
                _attachmentFilePaths.Add(attachmentFilePath);
            }
            else
                _attachmentFilePaths = new List<DocumentFilePath>() { attachmentFilePath };

            AddEvent(new NewAttachmentAdded(this, attachmentFilePath));
        }

        public void AddNewAttachmentsRange(ICollection<DocumentFilePath> documentFilePaths)
        {
            foreach(DocumentFilePath documentFilePath in documentFilePaths)
            {
                AddNewAttachment(documentFilePath);
            }
        }
    }
}
