using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Document;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public sealed record DocumentContent : AbstractRecordName
    {
        public DocumentContent(string value) : base(value, 100000, new EmptyDocumentContentException(), new TooLongDocumentContentException(value, 100000))
        {
        }

        public static implicit operator DocumentContent(string content)
            => new(content);
    }
}