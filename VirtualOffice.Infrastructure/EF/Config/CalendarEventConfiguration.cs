﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class CalendarEventConfiguration : IEntityTypeConfiguration<CalendarEvent>
    {
        public void Configure(EntityTypeBuilder<CalendarEvent> builder)
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
                p => new ScheduleItemStartDate(p));

            builder.Property(e => e._EndDate).HasConversion(
                p => p.Value,
                p => new ScheduleItemEndDate(p));

            builder.Property(e => e.Version)
            .IsConcurrencyToken();

            builder.HasMany(e => e._AssignedEmployees)
                .WithMany();
        }
    }
}