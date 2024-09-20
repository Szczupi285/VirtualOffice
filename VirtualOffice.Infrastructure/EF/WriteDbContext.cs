using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
using VirtualOffice.Infrastructure.EF.Config;
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

            var AppUserConfig = new ApplicationUserConfiguration();
            var AppIdentityUserConfig = new AppIdentityUserConfiguration();
            var CalendarEventConfig = new CalendarEventConfiguration();
            var EmployeeTaskConfig = new EmployeeTaskConfiguration();
            var MeetingConfig = new MeetingConfiguration();
            var MessageConfig = new MessageConfiguration();
            var NoteConfig = new NoteConfiguration();
            var OfficeConfig = new OfficeConfiguration();
            var OrganizationConfig = new OrganizationConfiguration();
            var PrivateChatRoomConfig = new PrivateChatRoomConfiguration();
            var PrivateDocumentConfig = new PrivateDocumentConfiguration();
            var PublicChatRoomConfig = new PublicChatRoomConfiguration();
            var PublicDocumentConfig = new PublicDocumentConfiguration();
            var SubscriptionConfig = new SubscriptionConfiguration();

            builder.ApplyConfiguration(AppUserConfig);
            builder.ApplyConfiguration(AppIdentityUserConfig);
            builder.ApplyConfiguration(CalendarEventConfig);
            builder.ApplyConfiguration(EmployeeTaskConfig);
            builder.ApplyConfiguration(MeetingConfig);
            builder.ApplyConfiguration(MessageConfig);
            builder.ApplyConfiguration(NoteConfig);
            builder.ApplyConfiguration(OfficeConfig);
            builder.ApplyConfiguration(OrganizationConfig);
            builder.ApplyConfiguration(PrivateChatRoomConfig);
            builder.ApplyConfiguration(PrivateDocumentConfig);
            builder.ApplyConfiguration(PublicChatRoomConfig);
            builder.ApplyConfiguration(PublicDocumentConfig);
            builder.ApplyConfiguration(SubscriptionConfig);
        }
    }