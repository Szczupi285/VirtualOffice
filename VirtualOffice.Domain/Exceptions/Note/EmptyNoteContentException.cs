﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Note
{
    public class EmptyNoteContentException : VirtualOfficeException
    {
        public EmptyNoteContentException() : base("Note Content cannot be empty")
        {
        }
    }
}
