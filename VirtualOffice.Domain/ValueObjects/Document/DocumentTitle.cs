using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Document;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public sealed record DocumentTitle : AbstractRecordName
    {
        public DocumentTitle(string value) : base(value, 50, new EmptyDocumentTitleException(), new TooLongDocumentTitleException(value))
        {
        }

        public static implicit operator DocumentTitle(string title)
            => new(title);
    }
}