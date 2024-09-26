using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(
                p => p.Value,
                p => new ApplicationUserId(p));

            builder.Property(e => e._Name).HasConversion(
                p => p.Value,
                p => new ApplicationUserName(p));

            builder.Property(e => e._Surname).HasConversion(
                p => p.Value,
                p => new ApplicationUserSurname(p));
        }
    }
}