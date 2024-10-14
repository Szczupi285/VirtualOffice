using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class EmployeeTaskConfiguration : IEntityTypeConfiguration<EmployeeTask>
    {
        public void Configure(EntityTypeBuilder<EmployeeTask> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(
                p => p.Value,
                p => new ScheduleItemId(p));

            builder.Property(e => e._Title).HasConversion(
                p => p.Value,
                p => new ScheduleItemTitle(p));

            builder.Property(e => e._Description).HasConversion(
               p => p.Value,
               p => new ScheduleItemDescription(p));

            builder.Property(e => e._StartDate).HasConversion(
                p => p.Value,
                p => ScheduleItemStartDate.CreateWithoutValidation(p));

            builder.Property(e => e._EndDate).HasConversion(
                p => p.Value,
                p => ScheduleItemEndDate.CreateWithoutValidation(p));

            builder.Property(e => e.Version)
            .IsConcurrencyToken();

            builder.HasMany(e => e._AssignedEmployees)
                .WithMany();
        }
    }
}