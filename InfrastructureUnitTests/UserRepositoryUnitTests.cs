using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Infrastructure.EF;
using VirtualOffice.Infrastructure.EF.Repositories;

namespace InfrastructureUnitTests
{
    public class UserRepositoryUnitTests
    {
        private readonly WriteDbContext _dbContext;
        private readonly UserRepository _repository;
        private readonly Guid _guid1;
        private readonly Guid _guid2;
        private readonly Guid _guid3;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;

        public UserRepositoryUnitTests()
        {
            _guid1 = Guid.NewGuid();
            _guid2 = Guid.NewGuid();
            _guid3 = Guid.NewGuid();
            _user1 = new(_guid1, "NameOne", "SurnameOne");
            _user2 = new(_guid2, "NameTwo", "SurnameTwo");
            _user3 = new(_guid3, "NameThree", "SurnameThree");

            // setup in memory db
            var options = new DbContextOptionsBuilder<WriteDbContext>()
             .UseInMemoryDatabase(databaseName: "Test")
             .Options;

            _dbContext = new WriteDbContext(options);
            _repository = new UserRepository(_dbContext);
            _dbContext.Employees.AddRange(_user1, _user2, _user3);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_guid1);
            // Assert
            Assert.Equal(_user1, result);
        }

        [Fact]
        public async Task AddAsync_NewEmployee_ShouldContain()
        {
            // Arrange
            Guid tempGuid = Guid.NewGuid();
            ApplicationUser newUser = new(tempGuid, "NameOne", "SurnameOne");
            // Act
            await _repository.AddAsync(newUser);
            // Assert
            Assert.Contains(newUser, _dbContext.Employees);
        }

        [Fact]
        public async Task DeleteAsync_ExistingEmployee_ShouldNotContain()
        {
            // Act
            await _repository.DeleteAsync(_user1);
            // Assert
            Assert.DoesNotContain(_user1, _dbContext.Employees);
        }

        [Fact]
        public async Task UpdateAsync_ExistingEmployee_ShouldChangeSurname()
        {
            // Arrange
            var user = await _repository.GetByIdAsync(_guid1);
            var newSurname = "newSurname";
            user.EditSurname(newSurname);
            // Act
            await _repository.UpdateAsync(_user1);
            var userAfterUpd = await _dbContext.Employees.FirstAsync(x => x.Id == _user1.Id);

            // Assert
            Assert.Equal(newSurname, userAfterUpd._Surname);
        }
    }
}