using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Office;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(
                p => p.Value,
                p => new OfficeId(p));

            builder.Property(e => e._officeName).HasConversion(
                p => p.Value,
                p => new OfficeName(p));

            builder.Property(e => e._description).HasConversion(
                p => p.Value,
                p => new OfficeDescription(p));

            builder.HasMany(e => e._members)
               .WithMany();
        }
    }
}