using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.ApplicationUser
{
    public sealed record ApplicationUserName : AbstractRecordName
    {
        public ApplicationUserName(string value) : base(value, 30, new EmptyApplicationUserNameException(), new TooLongApplicationUserNameException(value))
        {
            // Since there is for example 1 letter chinese names we only require at least 1 letter
            // dot is still valid so we can allow abbreviations
            if (!value.All(c => char.IsLetter(c) || c == '.')
                && !value.Any(char.IsLetter) || value[0] == '.')
                throw new InvalidApplicationUserNameException(value);
        }

        public static implicit operator ApplicationUserName(string name)
            => new(name);
    }
}