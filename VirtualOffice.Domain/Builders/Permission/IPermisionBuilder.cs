using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Domain.Builders.Permission
{
    internal interface IPermisionBuilder
    {
        void SetCanAddEmployeeTask();

        void SetCanAddToOffice();

        void SetCanCreateOffice();

        void SetCanAddToOrganization();

        void SetCanCreateMeeting();

        void SetCanSendMassMessages();

        void SetCanDeleteFromOffice();

        void SetCanDeleteFromOrganization();

        void SetCanAddPermissions();

        void SetCanHandleSubscriptions();

        void SetCanCheckActivityLog();

        void SetCanAddEventToOfficeCalendar();

        void SetCanAddEventToOrganizationCalendar();

        void SetCanCreatePools();

        void SetCanCreateOrganizationWideDocuments();

        void SetCanDeletePublicDocuments();

        void SetCanShareDocumentsToWholeOffice();

        void SetCanShareDocumentsToWholeOrganization();

        PermissionsEnum GetPermissions();
    }
}