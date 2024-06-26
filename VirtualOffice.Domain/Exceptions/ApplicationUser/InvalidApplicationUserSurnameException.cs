﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ApplicationUser
{
    public class InvalidApplicationUserSurnameException : VirtualOfficeException
    {
        string Value;
        public InvalidApplicationUserSurnameException(string value) : base($"ApplicationUser Surname: {value} does not only contain letters")
        {
            Value = value;
        }
    }
}
