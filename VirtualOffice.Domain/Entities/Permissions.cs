using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.Permissions;

namespace VirtualOffice.Domain.Entities
{
    public class Permissions
    {
        public PermissionsId Id { get; private set; }

        public int _BitPermissions { get; private set; }

        public Permissions(Guid id, int permissions)
        {
            Id = id;
            _BitPermissions = permissions;
        }
        
        public void AddPermission(int bitPermission) 
        {
            _BitPermissions += bitPermission;
        }
    }
}
