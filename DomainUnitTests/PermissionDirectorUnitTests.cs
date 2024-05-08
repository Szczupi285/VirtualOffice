using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Builders.Permission;
using VirtualOffice.Domain.Consts;

namespace DomainUnitTests
{
    public class PermissionDirectorUnitTests
    {

        [Fact]
        public void ConstructEmployee_ReturnsCorrectPermissions()
        {
            var permissions = PermissionDirector.ConstructEmployee();

            Assert.Equal(PermissionsEnum.CanSendMassMessages | PermissionsEnum.CanShareDocumentsToWholeOffice, permissions);
        }

        [Fact]
        public void ConstructTeamLeader_ReturnsCorrectPermissions()
        {
            var permissions = PermissionDirector.ConstructTeamLeader();

            Assert.Equal(PermissionsEnum.CanSendMassMessages | PermissionsEnum.CanShareDocumentsToWholeOffice |
                            PermissionsEnum.CanAddEmployeeTask | PermissionsEnum.CanCreateMeeting |
                            PermissionsEnum.CanAddEventToOfficeCalendar | PermissionsEnum.CanCreatePools, permissions);
        }

        [Fact]
        public void ConstructManager_ReturnsCorrectPermissions()
        {
            var permissions = PermissionDirector.ConstructManager();

            Assert.Equal(PermissionsEnum.CanSendMassMessages | PermissionsEnum.CanShareDocumentsToWholeOffice |
                            PermissionsEnum.CanAddEmployeeTask | PermissionsEnum.CanCreateMeeting |
                            PermissionsEnum.CanAddEventToOfficeCalendar | PermissionsEnum.CanCreatePools |
                            PermissionsEnum.CanCheckActivityLog | PermissionsEnum.CanCreateOrganizationWideDocuments |
                            PermissionsEnum.CanShareDocumentsToWholeOrganization | PermissionsEnum.CanDeletePublicDocuments |
                            PermissionsEnum.CanAddEventToOrganizationCalendar, permissions);
        }

        [Fact]
        public void ConstructAdministrator_ReturnsCorrectPermissions()
        {
            var permissions = PermissionDirector.ConstructAdministrator();

            Assert.Equal(PermissionsEnum.CanAddEmployeeTask | PermissionsEnum.CanAddToOffice |
                            PermissionsEnum.CanCreateOffices| PermissionsEnum.CanAddToOrganization |
                            PermissionsEnum.CanCreateMeeting | PermissionsEnum.CanSendMassMessages |
                            PermissionsEnum.CanDeleteFromOffice | PermissionsEnum.CanDeleteFromOrganization |
                            PermissionsEnum.CanAddPermissions | PermissionsEnum.CanShareDocumentsToWholeOffice |
                            PermissionsEnum.CanAddEventToOfficeCalendar | PermissionsEnum.CanCreatePools |
                            PermissionsEnum.CanDeleteFromOffice | PermissionsEnum.CanCheckActivityLog |
                            PermissionsEnum.CanCreateOrganizationWideDocuments | PermissionsEnum.CanShareDocumentsToWholeOrganization |
                            PermissionsEnum.CanDeletePublicDocuments, permissions);
        }

        [Fact]
        public void ConstuctMainAdministator_ReturnsCorrectPermissions()
        {
            var permissions = PermissionDirector.ConstuctMainAdministator();

            Assert.Equal(PermissionsEnum.CanAddEmployeeTask | PermissionsEnum.CanAddToOffice |
                            PermissionsEnum.CanCreateOffices | PermissionsEnum.CanAddToOrganization |
                            PermissionsEnum.CanCreateMeeting | PermissionsEnum.CanSendMassMessages |
                            PermissionsEnum.CanDeleteFromOffice | PermissionsEnum.CanDeleteFromOrganization |
                            PermissionsEnum.CanAddPermissions | PermissionsEnum.CanShareDocumentsToWholeOffice |
                            PermissionsEnum.CanAddEventToOfficeCalendar | PermissionsEnum.CanCreatePools |
                            PermissionsEnum.CanDeleteFromOffice | PermissionsEnum.CanCheckActivityLog |
                            PermissionsEnum.CanCreateOrganizationWideDocuments | PermissionsEnum.CanShareDocumentsToWholeOrganization |
                            PermissionsEnum.CanDeletePublicDocuments | PermissionsEnum.CanHandleSubscriptions, permissions);
        }
    }
}


