﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class TooLongDocumentFilePathException : VirtualOfficeException
    {
        string Value;
        public TooLongDocumentFilePathException(string value, uint length) : base($"File Path: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}
