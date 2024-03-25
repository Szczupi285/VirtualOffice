using VirtualOffice.Domain.Exceptions.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.ApplicationUser
{
    public sealed record ApplicationUserSurname
    {
        public string Value { get; }

        public ApplicationUserSurname(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyApplicationUserSurnameException();
            else if (value.Length > 35)
                throw new TooLongApplicationUserSurnameException(value);
            // We don't allow abbreviations in surname as we do in name
            else if (!value.All(char.IsLetter))
                throw new InvalidApplicationUserSurnameException(value);

            Value = value.Trim();
        }

        public static implicit operator string(ApplicationUserSurname surname)
            => surname.Value;

        public static implicit operator ApplicationUserSurname(string surname)
            => new(surname);

    }
}
