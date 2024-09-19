using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ChatRoom;
using VirtualOffice.Domain.ValueObjects.Document;
using VirtualOffice.Domain.ValueObjects.Message;
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

            builder.Entity<PrivateChatRoom>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new ChatRoomId(p));

                entity.HasMany(e => e._Participants)
                 .WithMany();

                entity.HasMany(e => e._Messages)
                .WithMany();
            });

            builder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new MessageId(p));

                entity.Property(e => e.Content).HasConversion(
                    p => p.Value,
                    p => new MessageContent(p));

                entity.HasOne(e => e.Sender)
                .WithMany()
                .HasForeignKey("SentByUserId")
                .IsRequired();
            });

            builder.Entity<PrivateDocument>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new DocumentId(p));

                entity.Property(e => e._title).HasConversion(
                    p => p.Value,
                    p => new DocumentTitle(p));

                entity.Property(e => e._content).HasConversion(
                    p => p.Value,
                    p => new DocumentContent(p));

                entity.Property(e => e._creationDate).HasConversion(
                    p => p.Value,
                    p => new DocumentCreationDate(p));

                entity.OwnsMany(e => e._attachmentFilePaths, a =>
                {
                    a.Property(fp => fp.Value)
                        .HasColumnName("FilePath")
                        .HasConversion(
                         p => p,
                         p => new DocumentFilePath(p));
                });
            });

            builder.Entity<PublicChatRoom>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new ChatRoomId(p));

                entity.Property(e => e._Name).HasConversion(
                    p => p.Value,
                    p => new PublicChatRoomName(p));

                entity.HasMany(e => e._Participants)
                 .WithMany();

                entity.HasMany(e => e._Messages)
                .WithMany();
            });

            builder.Entity<PublicDocument>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasConversion(
                    p => p.Value,
                    p => new DocumentId(p));

                entity.Property(e => e._title).HasConversion(
                   p => p.Value,
                   p => new DocumentTitle(p));

                entity.Property(e => e._content).HasConversion(
                    p => p.Value,
                    p => new DocumentContent(p));

                entity.OwnsMany(e => e._attachmentFilePaths, a =>
                {
                    a.Property(e => e)
                        .HasColumnName("FilePath")
                        .HasConversion(
                         p => p.Value,
                         p => new DocumentFilePath(p));
                });

                // since for document access premission reasons we need only userId
                // we don't create relationship with ApplicationUser entity since name, surname etc..
                // wouldn't be usefull in this scenario
                entity.OwnsMany(e => e._eligibleForRead, a =>
                {
                    a.ToTable("PublicDocumentEligibleForRead");

                    a.Property(e => e)
                        .HasColumnName("UserId")
                        .HasConversion(
                            p => p.Value,
                            p => new ApplicationUserId(p));
                });

                entity.OwnsMany(e => e._eligibleForWrite, a =>
                {
                    a.ToTable("PublicDocumentEligibleForWrite");

                    a.Property(e => e)
                    .HasColumnName("UserId")
                    .HasConversion(
                        p => p.Value,
                        p => new ApplicationUserId(p));
                });

                entity.OwnsOne(e => e._creationDetails, a =>
                {
                    a.ToTable("DocumentCreationDetails");

                    a.Property(e => e.DocumentCreationDate)
                        .HasColumnName("CreationDate")
                        .HasConversion(
                        p => p.Value,
                        p => new DocumentCreationDate(p));

                    a.Property(e => e.UserId)
                        .HasColumnName("CreatedByUserId")
                        .HasConversion(
                        p => p.Value,
                        p => new ApplicationUserId(p));
                });
            });
        }
    }
}