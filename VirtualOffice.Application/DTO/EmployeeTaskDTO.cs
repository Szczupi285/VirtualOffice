using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Application.DTO
{
    public class EmployeeTaskDTO
    {
        Guid Id { get; }
        public string _Title { get; } 
        public string _Description { get; } 
        public List<ApplicationUserDTO> _AssignedEmployees { get; }
        public string _Priority { get; } 
        public string Status { get; }
        public DateTime _StartDate { get; }
        public DateTime _EndDate { get; }


    }
}
