﻿using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class CantRemoveOnlyUserException : VirtualOfficeException
    {
        private Entities.ApplicationUser User;

        public CantRemoveOnlyUserException(Entities.ApplicationUser user) : base($"Last user with Id: {user.Id} cannot be removed from organization. Organization must have at least 1 user")
        {
            User = user;
        }
    }
}