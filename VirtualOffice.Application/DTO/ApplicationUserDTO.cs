using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Application.DTO
{
    public class ApplicationUserDTO
    {
        Guid Id { get; }
        public string _Name { get; }
        public string _Surname { get; }
    }
}
