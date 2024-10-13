using VirtualOffice.Application.DTO.ApplicationUser;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Application.DTO.EmployeeTask
{
    public class EmployeeTaskDTO
    {
        public string _Title { get; init; }
        public string _Description { get; init; }
        public HashSet<ApplicationUserDTO> _AssignedEmployees { get; init; }
        public EmployeeTaskPriorityEnum _Priority { get; init; }
        public DateTime _StartDate { get; init; }
        public DateTime _EndDate { get; init; }
    }
}