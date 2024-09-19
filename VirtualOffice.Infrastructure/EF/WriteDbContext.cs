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
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VirtualOffice.Infrastructure.Identity;

namespace VirtualOffice.Infrastructure.EF
{
    public class WriteDbContext : IdentityDbContext<AppIdentityUser, IdentityRole<Guid>, Guid>
    {
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
            });
        }
    }
}