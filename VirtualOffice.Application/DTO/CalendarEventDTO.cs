using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Application.DTO
{
    public class CalendarEventDTO
    {
        public Guid Id { get; }
        public string _Title { get; }
        public string _Description { get; }
        public HashSet<ApplicationUserDTO> _AssignedEmployees { get; }
        public DateTime _StartDate { get; }
        public DateTime _EndDate { get; }
    }
}
