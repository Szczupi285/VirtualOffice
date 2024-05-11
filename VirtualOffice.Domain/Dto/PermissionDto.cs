using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Services;

namespace VirtualOffice.Domain.Dto
{
    public class PermissionDto
    {
        public ICollection<string> _Permissions { get;}

        public PermissionDto(PermissionService permissionService)
        {
            List<PermissionsEnum> allPermissions = new List<PermissionsEnum>();
            PermissionsEnum PerEnum = permissionService.GetAllPermissions();

            foreach (PermissionsEnum permission in Enum.GetValues(typeof(PermissionsEnum)))
            {
                if (permissionService.HasPermission(permission))
                {
                    allPermissions.Add(permission);
                }
            }
            _Permissions = new List<string>(allPermissions.Select(p => p.ToString()));
        }
    }
}
