﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.EmployeeTask
{
    public class TooLongEmployeeTaskTitleException : VirtualOfficeException
    {
        string Value;
        public TooLongEmployeeTaskTitleException(string value, uint length) : base($"EmployeeTask Title: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}
