using VirtualOffice.Domain.Builders.Permission;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Services;

namespace DomainUnitTests
{
    public class PermissionServiceUnitTests
    {
        PermissionsEnum _Permissions;
        PermissionService _PermissionService;
        public PermissionServiceUnitTests()
        {
            PermissionBuilder builder = new PermissionBuilder();
            builder.SetCanAddEmployeeTask();
            builder.SetCanAddEventToOfficeCalendar();
            builder.SetCanAddEventToOrganizationCalendar();
            builder.SetCanAddToOffice();
            builder.SetCanCheckActivityLog();
            _Permissions = builder.GetPermissions();
            _PermissionService = new PermissionService(_Permissions);
        }

        [Fact]
        public void PermissionService_HasPermission_SettedCanAddEmployeeTask_ShouldReturnTrue()
        {
            Assert.True(_PermissionService.HasPermission(PermissionsEnum.CanAddEmployeeTask));
        }
        [Fact]
        public void PermissionService_NotSettedPermission_ShouldReturnFalse()
        {
            Assert.False(_PermissionService.HasPermission(PermissionsEnum.CanCreateOffices));
        }
        [Fact]
        public void PermissionService_GetAllPermissions_PermissionsShouldMatch()
        {
            Assert.Equal(_Permissions, _PermissionService.GetAllPermissions());
        }
    }
}
