using VirtualOffice.Application.Interfaces;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Application.Models
{
    public class EmployeeTaskReadModel : EntityId
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<EmployeeReadModel> AssignedEmployees { get; set; }
        public EmployeeTaskPriorityEnum Priority { get; set; }
        public EmployeeTaskStatusEnum Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}