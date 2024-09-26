using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Entities;
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
        public DbSet<PublicChatRoom> PublicChatRooms { get; set; }
        public DbSet<PublicDocument> PublicDocuments { get; set; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var configuration = new object[]
            {
                new ApplicationUserConfiguration(),
                new AppIdentityUserConfiguration(),
                new CalendarEventConfiguration(),
                new EmployeeTaskConfiguration(),
                new MeetingConfiguration(),
                new MessageConfiguration(),
                new NoteConfiguration(),
                new OfficeConfiguration(),
                new OrganizationConfiguration(),
                new PrivateChatRoomConfiguration(),
                new PublicChatRoomConfiguration(),
                new PublicDocumentConfiguration(),
            };

            foreach (var config in configuration)
            {
                builder.ApplyConfiguration((dynamic)config);
            }
        }
    }
}