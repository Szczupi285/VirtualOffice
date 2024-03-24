using VirtualOffice.Domain.Exceptions.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.ApplicationUser
{
    public sealed record ApplicationUserName
    {
        public string Value { get; }

        public ApplicationUserName(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyApplicationUserNameException();
            else if (value.Length > 50)
                throw new InvalidApplicationUserNameException(value);
            Value = value;
        }

        public static implicit operator string(ApplicationUserName name)
            => name.Value;

        public static implicit operator ApplicationUserName(string name)
            => new(name);

    }
}
