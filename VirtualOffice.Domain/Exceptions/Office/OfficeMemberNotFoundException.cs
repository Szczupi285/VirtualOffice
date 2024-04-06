using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Office
{
    public class OfficeMemberNotFoundException : VirtualOfficeException
    {
        string Value;

        public OfficeMemberNotFoundException(string value) : base($"Cannot find office member with the provided input: {value}.")
        {
            Value = value;
        }
    }
}
