using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Infrastructure.EF.Repositories;
using VirtualOffice.Infrastructure.EF;

namespace InfrastructureUnitTests
{
    public class MeetingRepositoryUnitTests
    {
        private readonly WriteDbContext _dbContext;
        private readonly MeetingRepository _repository;
        private readonly Guid _meetGuid1;
        private readonly Guid _meetGuid2;
        private readonly Guid _meetGuid3;
        private readonly Guid _guid1;
        private readonly Guid _guid2;
        private readonly Guid _guid3;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;
        private readonly List<Meeting> _data;

        public MeetingRepositoryUnitTests()
        {
            _meetGuid1 = Guid.NewGuid();
            _meetGuid2 = Guid.NewGuid();
            _meetGuid3 = Guid.NewGuid();

            _guid1 = Guid.NewGuid();
            _guid2 = Guid.NewGuid();
            _guid3 = Guid.NewGuid();
            _user1 = new(_guid1, "NameOne", "SurnameOne");
            _user2 = new(_guid2, "NameTwo", "SurnameTwo");
            _user3 = new(_guid3, "NameThree", "SurnameThree");

            _data = new List<Meeting>
            {
                new(_meetGuid1, "title", "description",
                new HashSet<ApplicationUser> { _user1, _user2 }, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8)),

                new(_meetGuid2, "title", "description",
                new HashSet<ApplicationUser> { _user2, _user3 }, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8)),

                new(_meetGuid3, "title", "description",
                new HashSet<ApplicationUser> { _user3, _user1 }, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8)),
            };

            // setup in memory db
            var options = new DbContextOptionsBuilder<WriteDbContext>()
             .UseInMemoryDatabase(databaseName: "Test")
             .Options;

            _dbContext = new WriteDbContext(options);
            _repository = new MeetingRepository(_dbContext);
            _dbContext.Meetings.AddRange(_data[0], _data[1], _data[2]);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_meetGuid1);
            // Assert
            Assert.Equal(_data[0], result);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_AssignedEmployeesShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_meetGuid1);
            // Assert
            Assert.Equal(_data[0]._AssignedEmployees, result._AssignedEmployees);
        }

        [Fact]
        public async Task AddAsync_NewMeeting_ShouldContain()
        {
            //Assert
            Guid testGuid = Guid.NewGuid();
            var Meeting = new Meeting(testGuid, "title", "description",
                new HashSet<ApplicationUser> { _user1, _user2 }, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8));
            // Act
            await _repository.AddAsync(Meeting);
            // Assert
            Assert.Contains(Meeting, _dbContext.Meetings);
        }

        [Fact]
        public async Task RemoveAsync_Meeting_ShouldNotContain()
        {
            // Arrange

            // Act
            Assert.Contains(_data[0], _dbContext.Meetings);
            await _repository.DeleteAsync(_data[0]);
            // Assert
            Assert.DoesNotContain(_data[0], _dbContext.Meetings);
        }

        [Fact]
        public async Task Update_Meeting_ShouldChangeTitle()
        {
            // Arrange
            var Meeting = await _repository.GetByIdAsync(_meetGuid1);
            string newTitle = "ChangedTitle";
            Meeting.SetTitle(newTitle);
            // Act
            await _repository.UpdateAsync(Meeting);
            var updatedMeeting = _dbContext.Meetings.First(x => x.Id == Meeting.Id);
            // Assert
            Assert.Equal(newTitle, updatedMeeting._Title);
        }

        [Fact]
        public async Task Update_Meeting_ShouldChangeDescription()
        {
            // Arrange
            var Meeting = await _repository.GetByIdAsync(_meetGuid1);
            string newDesc = "ChangedDesc";
            Meeting.SetDescription(newDesc);
            // Act
            await _repository.UpdateAsync(Meeting);
            var updatedMeeting = _dbContext.Meetings.First(x => x.Id == Meeting.Id);
            // Assert
            Assert.Equal(newDesc, updatedMeeting._Description);
        }

        [Fact]
        public async Task Update_Meeting_ShouldRemoveUser()
        {
            // Arrange
            var Meeting = await _repository.GetByIdAsync(_meetGuid1);
            Meeting.RemoveEmployee(_user1);
            // Act
            await _repository.UpdateAsync(Meeting);
            var updatedMeeting = _dbContext.Meetings.First(x => x.Id == Meeting.Id);
            // Assert
            Assert.Single(updatedMeeting._AssignedEmployees);
        }
    }
}