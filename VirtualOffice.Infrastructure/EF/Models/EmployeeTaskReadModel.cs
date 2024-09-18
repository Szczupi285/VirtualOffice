using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Infrastructure.EF.Models
{
    public class EmployeeTaskReadModel
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<UserReadModel> AssignedEmployees { get; set; }
        public EmployeeTaskPriorityEnum Priority { get; set; }
        public EmployeeTaskStatusEnum Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}