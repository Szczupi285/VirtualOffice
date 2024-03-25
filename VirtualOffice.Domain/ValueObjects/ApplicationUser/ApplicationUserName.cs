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
            else if (value.Length > 30)
                throw new TooLongApplicationUserNameException(value);
            // Since there is for example 1 letter chinese names we only require at least 1 letter
            // dot is still valid so we can allow abbreviations 
            else if (!value.All(c => char.IsLetter(c) || c == '.') 
                && !value.Any(char.IsLetter) || value[0] == '.')
                throw new InvalidApplicationUserNameException(value);

            Value = value.Trim();
        }

        public static implicit operator string(ApplicationUserName name)
            => name.Value;

        public static implicit operator ApplicationUserName(string name)
            => new(name);

    }
}
