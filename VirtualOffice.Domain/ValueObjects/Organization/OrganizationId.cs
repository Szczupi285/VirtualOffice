using VirtualOffice.Domain.Exceptions.Organization;

namespace VirtualOffice.Domain.ValueObjects.Organization
{
    public sealed record OrganizationId
    {
        public Guid Value { get; }

        public OrganizationId(Guid value)
        {
            if (value == Guid.Empty)
                throw new EmptyOrganizationIdException();

            Value = value;
        }

        public static implicit operator Guid(OrganizationId id)
            => id.Value;

        public static implicit operator OrganizationId(Guid id)
            => new(id);
    }
}
}
