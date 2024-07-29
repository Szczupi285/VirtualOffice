using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Application.DTO.ApplicationUser
{
    public class ApplicationUserDTO
    {
        Guid Id { get; init; }
        public string _Name { get; init; }
        public string _Surname { get; init; }
    }
}
