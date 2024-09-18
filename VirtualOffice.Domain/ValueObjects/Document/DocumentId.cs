using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Document;

namespace VirtualOffice.Domain.ValueObjects.Document
{
    public sealed record DocumentId : AbstractRecordId
    {
        public DocumentId(Guid value) : base(value, new EmptyDocumentIdException())
        {
        }

        public static implicit operator DocumentId(Guid id)
            => new(id);

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}