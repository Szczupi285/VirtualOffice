using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Domain.Consts
{
    public enum PermissionsEnum
    {
        None = 0,
        CanAddEmployeeTask = 1 << 0, // 1
        CanAddToOffice = 1 << 1, // 2
        CanCreateOffices = 1 << 2, // 4
        CanAddToOrganization = 1 << 3, // 8
        CanCreateMeeting = 1 << 4, // 16
        CanSendMassMessages = 1 << 5, // 32
        CanDeleteFromOffice = 1 << 6, // 64
        CanDeleteFromOrganization = 1 << 7, // 128
        CanAddPermissions = 1 << 8, // 256
        CanHandleSubscriptions = 1 << 9, // 512
        CanCheckActivityLog = 1 << 10, // 1024
        CanAddEventToOfficeCalendar = 1 << 11, // 2048
        CanAddEventToOrganizationCalendar = 1 << 12, // 4096
        CanCreatePools = 1 << 13, // 8192
        CanCreateOrganizationWideDocuments = 1 << 14, // 16384
        CanDeletePublicDocuments = 1 << 15, // 32768
        CanShareDocumentsToWholeOffice = 1 << 16, // 65536
        CanShareDocumentsToWholeOrganization = 1 << 17 // 131072
    }
}
