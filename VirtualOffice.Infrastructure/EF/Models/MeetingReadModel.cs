using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Infrastructure.EF.Models
{
    public class MeetingReadModel
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<UserReadModel> AssignedEmployees { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}