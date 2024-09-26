using VirtualOffice.Domain.Builders.Permission;
using VirtualOffice.Domain.Consts;

namespace DomainUnitTests
{
    public class PermissionBuilderUnitTests
    {
        [Fact]
        public void PermissionBuilder_Permissions_StartWithNone()
        {
            var permissionBuilder = new PermissionBuilder();
            var permissions = permissionBuilder.GetPermissions();
            Assert.Equal(PermissionsEnum.None, permissions);
        }

        [Fact]
        public void PermissionBuilder_SetCanAddEmployeeTask_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanAddEmployeeTask();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanAddEmployeeTask) == PermissionsEnum.CanAddEmployeeTask);
        }

        [Fact]
        public void PermissionBuilder_SetCanAddToOffice_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanAddToOffice();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanAddToOffice) == PermissionsEnum.CanAddToOffice);
        }

        [Fact]
        public void PermissionBuilder_SetCanCreateOffices_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanCreateOffice();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanCreateOffices) == PermissionsEnum.CanCreateOffices);
        }

        [Fact]
        public void PermissionBuilder_SetCanAddToOrganization_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanAddToOrganization();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanAddToOrganization) == PermissionsEnum.CanAddToOrganization);
        }

        [Fact]
        public void PermissionBuilder_SetCanCreateMeeting_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanCreateMeeting();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanCreateMeeting) == PermissionsEnum.CanCreateMeeting);
        }

        [Fact]
        public void PermissionBuilder_SetCanSendMassMessages_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanSendMassMessages();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanSendMassMessages) == PermissionsEnum.CanSendMassMessages);
        }

        [Fact]
        public void PermissionBuilder_SetCanDeleteFromOffice_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanDeleteFromOffice();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanDeleteFromOffice) == PermissionsEnum.CanDeleteFromOffice);
        }

        [Fact]
        public void PermissionBuilder_SetCanDeleteFromOrganization_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanDeleteFromOrganization();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanDeleteFromOrganization) == PermissionsEnum.CanDeleteFromOrganization);
        }

        [Fact]
        public void PermissionBuilder_SetCanAddPermissions_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanAddPermissions();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanAddPermissions) == PermissionsEnum.CanAddPermissions);
        }

        [Fact]
        public void PermissionBuilder_SetCanHandleSubscriptions_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanHandleSubscriptions();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanHandleSubscriptions) == PermissionsEnum.CanHandleSubscriptions);
        }

        [Fact]
        public void PermissionBuilder_SetCanCheckActivityLog_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanCheckActivityLog();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanCheckActivityLog) == PermissionsEnum.CanCheckActivityLog);
        }

        [Fact]
        public void PermissionBuilder_SetCanAddEventToOfficeCalendar_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanAddEventToOfficeCalendar();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanAddEventToOfficeCalendar) == PermissionsEnum.CanAddEventToOfficeCalendar);
        }

        [Fact]
        public void PermissionBuilder_SetCanAddEventToOrganizationCalendar_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanAddEventToOrganizationCalendar();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanAddEventToOrganizationCalendar) == PermissionsEnum.CanAddEventToOrganizationCalendar);
        }

        [Fact]
        public void PermissionBuilder_SetCanCreatePools_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanCreatePools();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanCreatePools) == PermissionsEnum.CanCreatePools);
        }

        [Fact]
        public void PermissionBuilder_SetCanCreateOrganizationWideDocuments_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanCreateOrganizationWideDocuments();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanCreateOrganizationWideDocuments) == PermissionsEnum.CanCreateOrganizationWideDocuments);
        }

        [Fact]
        public void PermissionBuilder_SetCanDeletePublicDocuments_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanDeletePublicDocuments();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanDeletePublicDocuments) == PermissionsEnum.CanDeletePublicDocuments);
        }

        [Fact]
        public void PermissionBuilder_SetCanShareDocumentsToWholeOffice_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanShareDocumentsToWholeOffice();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanShareDocumentsToWholeOffice) == PermissionsEnum.CanShareDocumentsToWholeOffice);
        }

        [Fact]
        public void PermissionBuilder_SetCanShareDocumentsToWholeOrganization_AddsCorrectPermission()
        {
            var permissionBuilder = new PermissionBuilder();
            permissionBuilder.SetCanShareDocumentsToWholeOrganization();
            var permissions = permissionBuilder.GetPermissions();
            Assert.True((permissions & PermissionsEnum.CanShareDocumentsToWholeOrganization) == PermissionsEnum.CanShareDocumentsToWholeOrganization);
        }
    }
}