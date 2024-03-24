using VirtualOffice.Domain.Exceptions.Organization;

namespace VirtualOffice.Domain.ValueObjects.Organization
{
    public sealed record OrganizationName
    {
        public string Value { get; }

        public OrganizationName(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyOrganizationNameException();
            else if (value.Length > 100)
                 throw new InvalidOrganizationNameException(value);

            Value = value;
        }

        public static implicit operator string(OrganizationName name)
            => name.Value;

        public static implicit operator OrganizationName(string name)
            => new(name);

    }
}
