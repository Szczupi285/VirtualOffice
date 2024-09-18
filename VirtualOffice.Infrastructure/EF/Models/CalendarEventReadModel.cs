using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualOffice.Infrastructure.EF.Models
{
    public class CalendarEventReadModel
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }
        public ICollection<UserReadModel> Users { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}