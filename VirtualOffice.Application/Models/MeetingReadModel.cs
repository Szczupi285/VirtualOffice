using VirtualOffice.Application.Interfaces;

namespace VirtualOffice.Application.Models
{
    public class MeetingReadModel : EntityId
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<EmployeeReadModel> AssignedEmployees { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}