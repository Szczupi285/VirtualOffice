using Microsoft.AspNetCore.Identity;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Infrastructure.Identity
{
    public class AppIdentityUser : IdentityUser<Guid>
    {
        public ApplicationUserId ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public AppIdentityUser()
        { }

        public AppIdentityUser(ApplicationUser applicationUser)
        {
            Id = ApplicationUser.Id;
            ApplicationUserId = applicationUser.Id;
        }
    }
}