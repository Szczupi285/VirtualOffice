using Microsoft.EntityFrameworkCore;
using Moq;
using System.Xml;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Interfaces;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;
using VirtualOffice.Infrastructure.EF;
using VirtualOffice.Infrastructure.EF.Repositories;
using ZstdSharp.Unsafe;

namespace InfrastructureUnitTests
{
    public class CalendarEventRepositoryUnitTests
    {
        private readonly WriteDbContext _dbContext;
        private readonly CalendarEventRepository _repository;
        private readonly Guid _calGuid1;
        private readonly Guid _calGuid2;
        private readonly Guid _calGuid3;
        private readonly Guid _guid1;
        private readonly Guid _guid2;
        private readonly Guid _guid3;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;
        private readonly List<CalendarEvent> _data;

        public CalendarEventRepositoryUnitTests()
        {
            _calGuid1 = Guid.NewGuid();
            _calGuid2 = Guid.NewGuid();
            _calGuid3 = Guid.NewGuid();

            _guid1 = Guid.NewGuid();
            _guid2 = Guid.NewGuid();
            _guid3 = Guid.NewGuid();
            _user1 = new(_guid1, "NameOne", "SurnameOne");
            _user2 = new(_guid2, "NameTwo", "SurnameTwo");
            _user3 = new(_guid3, "NameThree", "SurnameThree");

            _data = new List<CalendarEvent>
            {
                new CalendarEvent(_calGuid1, "title", "description",
                new HashSet<ApplicationUser> { _user1, _user2 }, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8)),

                new CalendarEvent(_calGuid2, "title", "description",
                new HashSet<ApplicationUser> { _user2, _user3 }, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8)),

                new CalendarEvent(_calGuid3, "title", "description",
                new HashSet<ApplicationUser> { _user3, _user1 }, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8)),
            };

            // setup in memory db
            var options = new DbContextOptionsBuilder<WriteDbContext>()
             .UseInMemoryDatabase(databaseName: "Test")
             .Options;

            _dbContext = new WriteDbContext(options);
            _repository = new CalendarEventRepository(_dbContext);
            _dbContext.CalendarEvents.AddRange(_data[0], _data[1], _data[2]);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_calGuid1);
            // Assert
            Assert.Equal(_data[0], result);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_AssignedEmployeesShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_calGuid1);
            // Assert
            Assert.Equal(_data[0]._AssignedEmployees, result._AssignedEmployees);
        }

        [Fact]
        public async Task AddAsync_NewCalendarEvent_ShouldContain()
        {
            //Assert
            Guid testGuid = Guid.NewGuid();
            var calEv = new CalendarEvent(testGuid, "title", "description",
                new HashSet<ApplicationUser> { _user1, _user2 }, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8));
            // Act
            await _repository.AddAsync(calEv);
            // Assert
            Assert.True(await _dbContext.CalendarEvents.ContainsAsync(calEv));
        }

        [Fact]
        public async Task RemoveAsync_CalendarEvent_ShouldNotContain()
        {
            // Arrange

            // Act
            Assert.True(await _dbContext.CalendarEvents.ContainsAsync(_data[0]));
            await _repository.DeleteAsync(_data[0]);
            // Assert
            Assert.False(await _dbContext.CalendarEvents.ContainsAsync(_data[0]));
        }

        [Fact]
        public async Task Update_CalendarEvent_ShouldChangeTitle()
        {
            // Arrange
            var calEv = await _repository.GetByIdAsync(_calGuid1);
            string newTitle = "ChangedTitle";
            calEv.SetTitle(newTitle);
            // Act
            await _repository.UpdateAsync(calEv);
            var updatedCalEv = _dbContext.CalendarEvents.First(x => x.Id == calEv.Id);
            // Assert
            Assert.Equal(newTitle, updatedCalEv._Title);
        }

        [Fact]
        public async Task Update_CalendarEvent_ShouldChangeDescription()
        {
            // Arrange
            var calEv = await _repository.GetByIdAsync(_calGuid1);
            string newDesc = "ChangedDesc";
            calEv.SetDescription(newDesc);
            // Act
            await _repository.UpdateAsync(calEv);
            var updatedCalEv = _dbContext.CalendarEvents.First(x => x.Id == calEv.Id);
            // Assert
            Assert.Equal(newDesc, updatedCalEv._Description);
        }

        [Fact]
        public async Task Update_CalendarEvent_ShouldRemoveUser()
        {
            // Arrange
            var calEv = await _repository.GetByIdAsync(_calGuid1);
            calEv.RemoveEmployee(_user1);
            // Act
            await _repository.UpdateAsync(calEv);
            var updatedCalEv = _dbContext.CalendarEvents.First(x => x.Id == calEv.Id);
            // Assert
            Assert.Single(updatedCalEv._AssignedEmployees);
        }
    }
}