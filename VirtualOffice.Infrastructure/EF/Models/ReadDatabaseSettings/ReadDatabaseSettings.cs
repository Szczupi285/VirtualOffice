using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Infrastructure.EF.Models.ReadDatabaseSettings
{
    public class ReadDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string EmployeesCollectionName { get; set; } = null!;
        public string CalendarEventsCollectionName { get; set; } = null!;
        public string EmployeeTasksCollectionName { get; set; } = null!;
        public string MeetingsCollectionName { get; set; } = null!;
        public string NotesCollectionName { get; set; } = null!;
        public string OfficesCollectionName { get; set; } = null!;
        public string OrganizationsCollectionName { get; set; } = null!;
        public string PrivateChatRoomsCollectionName { get; set; } = null!;
        public string PrivateDocumentsCollectionName { get; set; } = null!;
        public string PublicChatRoomsCollectionName { get; set; } = null!;
        public string PublicDocumentsCollectionName { get; set; } = null!;
    }
}