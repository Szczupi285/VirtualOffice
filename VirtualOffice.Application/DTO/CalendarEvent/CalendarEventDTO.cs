using VirtualOffice.Application.DTO.ApplicationUser;

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
