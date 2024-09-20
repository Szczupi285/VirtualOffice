using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Subscription;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(
                p => p.Value,
                p => new SubscriptionId(p));

            builder.Property(e => e._subStartDate).HasConversion(
                p => p.Value,
                p => new SubscriptionStartDate(p));

            builder.Property(e => e._subEndDate).HasConversion(
                p => p.Value,
                p => new SubscriptionEndDate(p));

            builder.Property(e => e._subscriptionFee).HasConversion(
                p => p.Value,
                p => new SubscriptionFee(p));
        }
    }
}