using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Organization;

namespace VirtualOffice.Domain.ValueObjects.Organization
{
    public sealed record OrganizationId : AbstractRecordId
    {
        public OrganizationId(Guid value) : base(value, new EmptyOrganizationIdException())
        {
        }

        public static implicit operator OrganizationId(Guid id)
            => new(id);
    }
}