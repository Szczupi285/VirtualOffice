﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Permission
{
    public class EmptyPermissionIdException : VirtualOfficeException
    {
        public EmptyPermissionIdException() : base(message)
        {
        }
    }
}
