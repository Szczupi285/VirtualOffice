using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Domain.ValueObjects.ApplicationUser
{
    public sealed record Permissions
    {
        public uint _BitPermissions { get; private set; }

        public void AddPermission(uint bitPermission)
        {
            _BitPermissions += bitPermission;
        }
    }
}
