﻿using VirtualOffice.Domain.Exceptions.ApplicationUser;

namespace VirtualOffice.Domain.ValueObjects.ApplicationUser
{
    public record ApplicationUserSurname
    {
        public string Value { get; }

        public ApplicationUserSurname(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyApplicationUserSurnameException();
            }

            Value = value;
        }

        public static implicit operator string(ApplicationUserSurname surname)
            => surname.Value;

        public static implicit operator ApplicationUserSurname(string surname)
            => new(surname);

    }
}
