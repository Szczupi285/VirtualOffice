using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Office;

namespace VirtualOffice.Domain.ValueObjects.Office
{
    public sealed record OfficeId : AbstractRecordId
    {
        public OfficeId(Guid value) : base(value, new EmptyOfficeIdException())
        {
        }

        public static implicit operator OfficeId(Guid id)
            => new(id);
    }
}