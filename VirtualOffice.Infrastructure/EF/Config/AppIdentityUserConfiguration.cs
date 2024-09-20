using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
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

            builder.Property(e => e.ApplicationUserId).HasConversion(
                p => p.Value,
                p => new ApplicationUserId(p)).HasColumnName("EmployeeId");
        }
    }
}