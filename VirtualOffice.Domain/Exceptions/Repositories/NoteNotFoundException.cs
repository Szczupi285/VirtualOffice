﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Repositories
{
    public class NoteNotFoundException : VirtualOfficeException
    {
        public NoteNotFoundException(Guid guid) : base($"Note with Id: {guid} has not been found")
        {
        }
    }
}