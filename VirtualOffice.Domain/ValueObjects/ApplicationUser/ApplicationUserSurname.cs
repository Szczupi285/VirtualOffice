﻿using VirtualOffice.Domain.Exceptions.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.ApplicationUser
{
    public sealed record ApplicationUserSurname
    {
        public string Value { get; }

        public ApplicationUserSurname(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                throw new EmptyApplicationUserSurnameException();
            else if (value.Length > 50)
                throw new InvalidApplicationUserSurnameException(value);

            Value = value;
        }

        public static implicit operator string(ApplicationUserSurname surname)
            => surname.Value;

        public static implicit operator ApplicationUserSurname(string surname)
            => new(surname);

    }
}
