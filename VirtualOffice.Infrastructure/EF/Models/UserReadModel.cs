using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Infrastructure.EF.Models
{
    public class UserReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public PermissionsEnum Permissions { get; set; }
        public CalendarEventReadModel CalendarEventReadModel { get; set; }
    }
}