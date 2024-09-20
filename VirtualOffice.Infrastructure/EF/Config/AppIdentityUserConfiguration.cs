using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.Identity;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class AppIdentityUserConfiguration : IEntityTypeConfiguration<AppIdentityUser>
    {
        public void Configure(EntityTypeBuilder<AppIdentityUser> builder)
        {
            builder.HasOne(e => e.ApplicationUser)
            .WithOne()
            .HasForeignKey<AppIdentityUser>(e => e.ApplicationUserId)
            .IsRequired();
        }
    }
}