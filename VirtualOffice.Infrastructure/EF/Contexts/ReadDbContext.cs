using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.EF.Models;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.EF.Contexts
{
    internal class ReadDbContext : DbContext
    {
        public DbSet<EmployeeReadModel> Employees { get; set; }
        public DbSet<CalendarEventReadModel> CalendarEvents { get; set; }
        public DbSet<EmployeeTaskReadModel> EmployeeTasks { get; set; }
        public DbSet<MeetingReadModel> Meetings { get; set; }
        public DbSet<NoteReadModel> Notes { get; set; }
        public DbSet<OfficeReadModel> Offices { get; set; }
        public DbSet<OrganizationReadModel> Organizations { get; set; }
        public DbSet<PrivateChatRoomReadModel> PrivateChatRooms { get; set; }
        public DbSet<PrivateDocumentReadModel> PrivateDocuments { get; set; }
        public DbSet<PublicChatRoomsService> PublicChatRooms { get; set; }
        public DbSet<PublicDocumentReadModel> PublicDocuments { get; set; }

        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}