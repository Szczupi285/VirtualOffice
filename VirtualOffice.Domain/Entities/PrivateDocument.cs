﻿using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Entities
{
    // object created using Builder
    public class PrivateDocument : AbstractDocument
    {
        public DocumentCreationDate _creationDate { get; private set; } = DateTime.UtcNow;

        public PrivateDocument()
        { }

        public PrivateDocumentMemento SaveToMemento()
        {
            return new PrivateDocumentMemento(Id, _title, _content, _attachmentFilePaths, _creationDate);
        }

        public void RestoreFromMemento(PrivateDocumentMemento memento)
        {
            // properies are not valdiated by add methods since they were already validated when the object was constructed and saved to memento
            Id = memento.Id;
            _title = memento._title;
            _content = memento._content;
            _attachmentFilePaths = memento._attachmentFilePaths;
            _creationDate = memento._creationDate;
        }
    }
}