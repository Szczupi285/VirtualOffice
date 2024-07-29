using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.ApplicationUser;

namespace VirtualOffice.Application.DTO.EmployeeTask
{
    public class EmployeeTaskDTO
    {
        Guid Id { get; init; }
        public string _Title { get; init; }
        public string _Description { get; init; }
        public List<ApplicationUserDTO> _AssignedEmployees { get; init; }
        public string _Priority { get; init; }
        public string Status { get; init; }
        public DateTime _StartDate { get; init; }
        public DateTime _EndDate { get; init; }
    }
}
