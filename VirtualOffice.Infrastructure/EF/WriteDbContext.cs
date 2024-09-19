using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
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
    }
}