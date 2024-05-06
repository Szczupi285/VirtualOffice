using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Dto;

namespace VirtualOffice.Domain.Services
{
    public class PermissionService
    {
        private PermissionsEnum _PermissionsEnum { get; }
        public PermissionService(PermissionsEnum permissions)
        {
            _PermissionsEnum = permissions;
        }

        public bool HasPermission(PermissionsEnum permission) => _PermissionsEnum.HasFlag(permission);
        public PermissionsEnum GetAllPermissions() => _PermissionsEnum;



    }
}
