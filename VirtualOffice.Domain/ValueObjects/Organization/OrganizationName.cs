using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.Exceptions.Organization;

namespace VirtualOffice.Domain.ValueObjects.Organization
{
    public sealed record OrganizationName : AbstractRecordName
    {

        public OrganizationName(string value) : base(value, 100, new EmptyOrganizationNameException(), new InvalidOrganizationNameException(value))
        {
        }

        public static implicit operator OrganizationName(string name)
            => new(name);

    }
}
