﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.NoteService
{
    public class NoteNotFoundException : VirtualOfficeException
    {
        string Value;
        public NoteNotFoundException(string value) : base($"Note with Id: {value} has not been found")
        {
            Value = value;
        }
    }
}
