using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Note;
using VirtualOffice.Domain.ValueObjects.Office;
using VirtualOffice.Domain.ValueObjects.Organization;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VirtualOffice.Domain.ValueObjects.Subscription;
using VirtualOffice.Infrastructure.Identity;

namespace VirtualOffice.Infrastructure.EF
{
    public class WriteDbContext : IdentityDbContext<AppIdentityUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<ApplicationUser> Employees { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<PrivateChatRoom> PrivateChatRooms { get; set; }
        public DbSet<PrivateDocument> PrivateDocuments { get; set; }
        public DbSet<PublicChatRoom> PublicChatRooms { get; set; }
        public DbSet<PublicDocument> PublicDocuments { get; set; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new ApplicationUserId(p));

                entity.Property(e => e._Name).HasConversion(
                    p => p.Value,
                    p => new ApplicationUserName(p));

                entity.Property(e => e._Surname).HasConversion(
                    p => p.Value,
                    p => new ApplicationUserSurname(p));
            });

            builder.Entity<AppIdentityUser>(entity =>
            {
                entity.HasOne(e => e.ApplicationUser)
                .WithOne()
                .HasForeignKey<AppIdentityUser>(e => e.ApplicationUserId)
                .IsRequired();
            });

            builder.Entity<CalendarEvent>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemId(p));

                entity.Property(e => e._Title).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemTitle(p));

                entity.Property(e => e._Description).HasConversion(
                   p => p.Value,
                   p => new ScheduleItemDescription(p));

                entity.Property(e => e._StartDate).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemStartDate(p));

                entity.Property(e => e._EndDate).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemEndDate(p));

                entity.HasMany(e => e._AssignedEmployees)
                    .WithMany();
            });

            builder.Entity<EmployeeTask>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemId(p));

                entity.Property(e => e._Title).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemTitle(p));

                entity.Property(e => e._Description).HasConversion(
                   p => p.Value,
                   p => new ScheduleItemDescription(p));

                entity.Property(e => e._StartDate).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemStartDate(p));

                entity.Property(e => e._EndDate).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemEndDate(p));

                entity.HasMany(e => e._AssignedEmployees)
                    .WithMany();
            });

            builder.Entity<Meeting>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemId(p));

                entity.Property(e => e._Title).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemTitle(p));

                entity.Property(e => e._Description).HasConversion(
                   p => p.Value,
                   p => new ScheduleItemDescription(p));

                entity.Property(e => e._StartDate).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemStartDate(p));

                entity.Property(e => e._EndDate).HasConversion(
                    p => p.Value,
                    p => new ScheduleItemEndDate(p));

                entity.HasMany(e => e._AssignedEmployees)
                    .WithMany();
            });

            builder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new NoteId(p));

                entity.Property(e => e._title).HasConversion(
                    p => p.Value,
                    p => new NoteTitle(p));

                entity.Property(e => e._content).HasConversion(
                    p => p.Value,
                    p => new NoteContent(p));

                entity.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey("CreatedByUserId")
                .IsRequired();
            });

            builder.Entity<Office>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new OfficeId(p));

                entity.Property(e => e._officeName).HasConversion(
                    p => p.Value,
                    p => new OfficeName(p));

                entity.Property(e => e._description).HasConversion(
                    p => p.Value,
                    p => new OfficeDescription(p));

                entity.HasMany(e => e._members)
                   .WithMany();
            });

            builder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Ignore(e => e._usedSlots);
                entity.Ignore(e => e._slotsLeft);
                entity.Ignore(e => e._userLimit);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new OrganizationId(p));

                entity.Property(e => e._name).HasConversion(
                    p => p.Value,
                    p => new OrganizationName(p));

                entity.HasMany(e => e._offices)
                .WithOne();

                entity.HasMany(e => e._organizationUsers)
                .WithOne();

                entity.HasOne(e => e._subscription)
                .WithOne();
            });
            builder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new SubscriptionId(p));

                entity.Property(e => e._subStartDate).HasConversion(
                    p => p.Value,
                    p => new SubscriptionStartDate(p));

                entity.Property(e => e._subEndDate).HasConversion(
                    p => p.Value,
                    p => new SubscriptionEndDate(p));

                entity.Property(e => e._subscriptionFee).HasConversion(
                    p => p.Value,
                    p => new SubscriptionFee(p));
            });
        }
    }
}