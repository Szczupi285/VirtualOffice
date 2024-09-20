using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Ignore(e => e._usedSlots);
            builder.Ignore(e => e._slotsLeft);
            builder.Ignore(e => e._userLimit);

            builder.Property(e => e.Id).HasConversion(
                p => p.Value,
                p => new OrganizationId(p));

            builder.Property(e => e._name).HasConversion(
                p => p.Value,
                p => new OrganizationName(p));

            builder.HasMany(e => e._offices)
                .WithOne();

            builder.HasMany(e => e._organizationUsers)
                .WithOne();

            builder.HasOne(e => e._subscription)
                .WithOne();
        }
    }
}