using Microsoft.EntityFrameworkCore;
using Moq;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VirtualOffice.Infrastructure.EF;
using VirtualOffice.Infrastructure.EF.Repositories;

namespace InfrastructureUnitTests
{
    public class CalendarEventRepositoryUnitTests
    {
        private readonly Mock<WriteDbContext> _mockDbContext;
        private readonly Mock<DbSet<CalendarEvent>> _mockDbSet;

        public CalendarEventRepositoryUnitTests()
        {
            _mockDbContext = new Mock<WriteDbContext>();
            _mockDbSet = new Mock<DbSet<CalendarEvent>>();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsCalendarEvent()
        {
            // Arrange
            ScheduleItemId calendarExistingId = new(Guid.NewGuid());
            ApplicationUserId userExistingId = new(Guid.NewGuid());
            ApplicationUser user = new(userExistingId, "name", "surname");
            CalendarEvent CalendarEvent = new(calendarExistingId, "title", "description", new HashSet<ApplicationUser> { user }, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8));

            var queryableEvents = new List<CalendarEvent> { CalendarEvent }.AsQueryable();
            _mockDbSet.As<IQueryable<CalendarEvent>>().Setup(m => m.Provider).Returns(queryableEvents.Provider);
            _mockDbSet.As<IQueryable<CalendarEvent>>().Setup(m => m.Expression).Returns(queryableEvents.Expression);
            _mockDbSet.As<IQueryable<CalendarEvent>>().Setup(m => m.ElementType).Returns(queryableEvents.ElementType);
            _mockDbSet.As<IQueryable<CalendarEvent>>().Setup(m => m.GetEnumerator()).Returns(queryableEvents.GetEnumerator());

            _mockDbContext.Setup(c => c.CalendarEvents).Returns(_mockDbSet.Object);

            CalendarEventRepository repository = new(_mockDbContext.Object);
            // Act
            var result = await repository.GetByIdAsync(calendarExistingId);
            // Assert
            Assert.Equal(CalendarEvent, result);
        }
    }
}