using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.ApplicationUser;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Application.DTO.CalendarEvent
{
    public class CalendarEventDTO
    {
        public Guid Id { get; init; }
        public string _Title { get; init; }
        public string _Description { get; init; }
        public HashSet<ApplicationUserDTO> _AssignedEmployees { get; init; }
        public DateTime _StartDate { get; init; }
        public DateTime _EndDate { get; init; }
    }
}
