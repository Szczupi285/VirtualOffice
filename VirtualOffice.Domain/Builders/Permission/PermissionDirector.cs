using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Domain.Builders.Permission
{
    public static class PermissionDirector
    {

        public static PermissionsEnum ConstructEmployee()
        {
            PermissionBuilder builder = new PermissionBuilder();

            builder.SetCanSendMassMessages();
            builder.SetCanShareDocumentsToWholeOffice();
            return builder.GetPermissions();
        }

        public static PermissionsEnum ConstructTeamLeader()
        {
            PermissionBuilder builder = new PermissionBuilder();

            builder.SetCanSendMassMessages();
            builder.SetCanShareDocumentsToWholeOffice();
            builder.SetCanAddEmployeeTask();
            builder.SetCanCreateMeeting();
            builder.SetCanAddEventToOfficeCalendar();
            builder.SetCanCreatePools();

            return builder.GetPermissions();
        }

        public static PermissionsEnum ConstructManager()
        {
            PermissionBuilder builder = new PermissionBuilder();

            builder.SetCanSendMassMessages();
            builder.SetCanShareDocumentsToWholeOffice();
            builder.SetCanAddEmployeeTask();
            builder.SetCanCreateMeeting();
            builder.SetCanAddEventToOfficeCalendar();
            builder.SetCanCreatePools();
            builder.SetCanCheckActivityLog();
            builder.SetCanCreateOrganizationWideDocuments();
            builder.SetCanShareDocumentsToWholeOrganization();
            builder.SetCanDeletePublicDocuments();
            builder.SetCanAddEventToOrganizationCalendar();
            

            return builder.GetPermissions();
        }

        public static PermissionsEnum ConstructAdministrator()
        {
            PermissionBuilder builder = new PermissionBuilder();

            builder.SetCanAddEmployeeTask();
            builder.SetCanAddToOffice();
            builder.SetCanCreateOffice();
            builder.SetCanAddToOrganization();
            builder.SetCanCreateMeeting();
            builder.SetCanSendMassMessages();
            builder.SetCanDeleteFromOffice();
            builder.SetCanDeleteFromOrganization();
            builder.SetCanAddPermissions();
            builder.SetCanShareDocumentsToWholeOffice();
            builder.SetCanAddEventToOfficeCalendar();
            builder.SetCanCreatePools();
            builder.SetCanDeleteFromOffice();
            builder.SetCanCheckActivityLog();
            builder.SetCanCreateOrganizationWideDocuments();
            builder.SetCanShareDocumentsToWholeOrganization();
            builder.SetCanDeletePublicDocuments();

            return builder.GetPermissions();
        }

        public static PermissionsEnum ConstuctMainAdministator()
        {
            PermissionBuilder builder = new PermissionBuilder();

            builder.SetCanAddEmployeeTask();
            builder.SetCanAddToOffice();
            builder.SetCanCreateOffice();
            builder.SetCanAddToOrganization();
            builder.SetCanCreateMeeting();
            builder.SetCanSendMassMessages();
            builder.SetCanDeleteFromOffice();
            builder.SetCanDeleteFromOrganization();
            builder.SetCanAddPermissions();
            builder.SetCanShareDocumentsToWholeOffice();
            builder.SetCanAddEventToOfficeCalendar();
            builder.SetCanCreatePools();
            builder.SetCanDeleteFromOffice();
            builder.SetCanCheckActivityLog();
            builder.SetCanCreateOrganizationWideDocuments();
            builder.SetCanShareDocumentsToWholeOrganization();
            builder.SetCanDeletePublicDocuments();
            builder.SetCanHandleSubscriptions();

            return builder.GetPermissions();

        }
    }
}
