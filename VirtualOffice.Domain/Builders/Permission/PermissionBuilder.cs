using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Domain.Builders.Permission
{
    public class PermissionBuilder : IPermisionBuilder
    {
        private PermissionsEnum permissions = PermissionsEnum.None;

        public void Reset() => permissions = new PermissionsEnum();

        public PermissionBuilder()
        {
            Reset();
        }

        public void SetCanAddEmployeeTask()
        {
            permissions |= PermissionsEnum.CanAddEmployeeTask;
        }

        public void SetCanAddEventToOfficeCalendar()
        {
            permissions |= PermissionsEnum.CanAddEventToOfficeCalendar;
        }

        public void SetCanAddEventToOrganizationCalendar()
        {
            permissions |= PermissionsEnum.CanAddEventToOrganizationCalendar;
        }

        public void SetCanAddPermissions()
        {
            permissions |= PermissionsEnum.CanAddPermissions;
        }

        public void SetCanAddToOffice()
        {
            permissions |= PermissionsEnum.CanAddToOffice;
        }

        public void SetCanAddToOrganization()
        {
            permissions |= PermissionsEnum.CanAddToOrganization;
        }

        public void SetCanCheckActivityLog()
        {
            permissions |= PermissionsEnum.CanCheckActivityLog;
        }

        public void SetCanCreateMeeting()
        {
            permissions |= PermissionsEnum.CanCreateMeeting;
        }

        public void SetCanCreateOffice()
        {
            permissions |= PermissionsEnum.CanCreateOffices;
        }

        public void SetCanCreateOrganizationWideDocuments()
        {
            permissions |= PermissionsEnum.CanCreateOrganizationWideDocuments;
        }

        public void SetCanCreatePools()
        {
            permissions |= PermissionsEnum.CanCreatePools;
        }

        public void SetCanDeleteFromOffice()
        {
            permissions |= PermissionsEnum.CanDeleteFromOffice;
        }

        public void SetCanDeleteFromOrganization()
        {
            permissions |= PermissionsEnum.CanDeleteFromOrganization;
        }

        public void SetCanDeletePublicDocuments()
        {
            permissions |= PermissionsEnum.CanDeletePublicDocuments;
        }

        public void SetCanHandleSubscriptions()
        {
            permissions |= PermissionsEnum.CanHandleSubscriptions;
        }

        public void SetCanSendMassMessages()
        {
            permissions |= PermissionsEnum.CanSendMassMessages;
        }

        public void SetCanShareDocumentsToWholeOffice()
        {
            permissions |= PermissionsEnum.CanShareDocumentsToWholeOffice;
        }

        public void SetCanShareDocumentsToWholeOrganization()
        {
            permissions |= PermissionsEnum.CanShareDocumentsToWholeOrganization;
        }

        /// <summary>
        /// Builds a PermissionsEnumObject and returns it. <br/>
        /// Be aware that after object is reseted after calling this method.
        /// </summary>
        public PermissionsEnum GetPermissions()
        {
            PermissionsEnum result = permissions;
            Reset();
            return result;
        }
    }
}
