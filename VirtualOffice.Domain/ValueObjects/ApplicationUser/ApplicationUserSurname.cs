using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.ApplicationUser
{
    public sealed record ApplicationUserSurname : AbstractRecordName
    {
        public ApplicationUserSurname(string value) : base(value, 35, new EmptyApplicationUserSurnameException(), new TooLongApplicationUserSurnameException(value))
        {
            // We don't allow abbreviations in surname as we do in name
            if (!value.All(char.IsLetter))
                throw new InvalidApplicationUserSurnameException(value);
        }

        public static implicit operator ApplicationUserSurname(string surname)
            => new(surname);
    }
}