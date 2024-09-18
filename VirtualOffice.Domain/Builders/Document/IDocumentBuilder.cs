using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Builders.Document
{
    internal interface IDocumentBuilder
    {
        void SetId(Guid id);

        void SetTitle(string title);

        void SetContent(string content);

        void SetAttachments(ICollection<DocumentFilePath> attachmentFilePaths);
    }
}