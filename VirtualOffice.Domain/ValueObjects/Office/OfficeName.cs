using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Office;

namespace VirtualOffice.Domain.ValueObjects.Office
{
    public sealed record OfficeName : AbstractRecordName
    {
        public OfficeName(string value) : base(value, 50, new EmptyOfficeNameException(), new InvalidOfficeNameException(value))
        {
        }

        public static implicit operator OfficeName(string name)
            => new(name);
    }
}