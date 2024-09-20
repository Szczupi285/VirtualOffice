using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Organization;
using VirtualOffice.Domain.ValueObjects.Subscription;

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

            builder.OwnsOne(e => e._subscription, a =>
            {
                a.ToTable("Subscriptions");

                a.Property(e => e.Id).HasConversion(
                    p => p.Value,
                      p => new SubscriptionId(p));

                a.Property(e => e._subStartDate).HasConversion(
                    p => p.Value,
                    p => new SubscriptionStartDate(p));

                a.Property(e => e._subEndDate).HasConversion(
                    p => p.Value,
                    p => new SubscriptionEndDate(p));

                a.Property(e => e._subscriptionFee).HasConversion(
                    p => p.Value,
                    p => new SubscriptionFee(p));
            });
        }
    }
}