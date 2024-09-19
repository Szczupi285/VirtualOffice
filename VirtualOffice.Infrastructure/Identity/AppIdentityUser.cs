using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Infrastructure.Identity
{
    public class AppIdentityUser : IdentityUser<Guid>
    {
        public ApplicationUser ApplicationUser { get; set; }

        public AppIdentityUser()
        { }

        public AppIdentityUser(ApplicationUser applicationUser)
        {
            Id = ApplicationUser.Id.Value;
        }
    }
}