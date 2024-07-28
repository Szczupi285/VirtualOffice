using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Application.DTO
{
    public class MeetingDTO
    {
        Guid Id { get; init; }
        public string _Title { get; init; }
        public string _Description { get; init; }
        public List<ApplicationUserDTO> _AssignedEmployees { get; init; }
        public DateTime _StartDate { get; init; }
        public DateTime _EndDate { get; init; }

    }
}
