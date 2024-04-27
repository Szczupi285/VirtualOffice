using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Permission;

namespace VirtualOffice.Domain.ValueObjects.Permissions
{
    public sealed record PermissionsId : AbstractRecordId
    {
        public PermissionsId(Guid value) : base(value, new EmptyPermissionIdException())
        {
        }

        public static implicit operator PermissionsId(Guid id)
            => new(id);
    }
}
