using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ApplicationUser;
using VirtualOffice.Domain.Exceptions.Office;
using VirtualOffice.Domain.Exceptions.Organization;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.ValueObjects.Office
{
    public sealed record OfficeName : AbstractRecordName
    {
        public OfficeName(string value) : base(value, 50, new EmptyOfficeNameException(), new InvalidOfficeNameException(value))
        {
        }

        public static implicit operator OfficeName(string name)
            => new(name);
    }
}
