﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ApplicationUser
{
    public class InvalidApplicationUserNameException : VirtualOfficeException
    {
        string Value;
        public InvalidApplicationUserNameException(string value) 
            : base($"User Name: {value} is more than 50 characters long")
        {
            Value = value;
        }
    }
}
