using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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