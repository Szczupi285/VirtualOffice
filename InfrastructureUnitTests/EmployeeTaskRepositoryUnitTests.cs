using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Infrastructure.EF.Repositories;
using VirtualOffice.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Consts;

namespace InfrastructureUnitTests
{
    public class EmployeeTaskRepositoryUnitTests
    {
        private readonly WriteDbContext _dbContext;
        private readonly EmployeeTaskRepository _repository;
        private readonly Guid _empTaskGuid1;
        private readonly Guid _empTaskGuid2;
        private readonly Guid _empTaskGuid3;
        private readonly Guid _guid1;
        private readonly Guid _guid2;
        private readonly Guid _guid3;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;
        private readonly List<EmployeeTask> _data;

        public EmployeeTaskRepositoryUnitTests()
        {
            _empTaskGuid1 = Guid.NewGuid();
            _empTaskGuid2 = Guid.NewGuid();
            _empTaskGuid3 = Guid.NewGuid();

            _guid1 = Guid.NewGuid();
            _guid2 = Guid.NewGuid();
            _guid3 = Guid.NewGuid();
            _user1 = new(_guid1, "NameOne", "SurnameOne");
            _user2 = new(_guid2, "NameTwo", "SurnameTwo");
            _user3 = new(_guid3, "NameThree", "SurnameThree");

            _data = new List<EmployeeTask>
            {
                new EmployeeTask(_empTaskGuid1, "titleOne", "descriptionOne",
                new HashSet<ApplicationUser> { _user1, _user2 }, EmployeeTaskPriorityEnum.Low, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8)),

                new EmployeeTask(_empTaskGuid2, "titleTwo", "descriptionTwo",
                new HashSet<ApplicationUser> { _user2, _user3 }, EmployeeTaskPriorityEnum.Low,DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8)),

                new EmployeeTask(_empTaskGuid3, "titleThree", "descriptionThree",
                new HashSet<ApplicationUser> { _user3, _user1 }, EmployeeTaskPriorityEnum.Low,DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8)),
            };

            // setup in memory db
            var options = new DbContextOptionsBuilder<WriteDbContext>()
             .UseInMemoryDatabase(databaseName: "Test")
             .Options;

            _dbContext = new WriteDbContext(options);
            _repository = new EmployeeTaskRepository(_dbContext);
            _dbContext.EmployeeTasks.AddRange(_data[0], _data[1], _data[2]);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_empTaskGuid1);
            // Assert
            Assert.Equal(_data[0], result);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_AssignedEmployeesShouldEqual()
        {
            // Act
            var result = await _repository.GetByIdAsync(_empTaskGuid1);
            // Assert
            Assert.Equal(_data[0]._AssignedEmployees, result._AssignedEmployees);
        }

        [Fact]
        public async Task AddAsync_NewEmployeeTask_ShouldContain()
        {
            //Assert
            Guid testGuid = Guid.NewGuid();
            var EmpTask = new EmployeeTask(testGuid, "title", "description",
                new HashSet<ApplicationUser> { _user1, _user2 }, EmployeeTaskPriorityEnum.Low, DateTime.UtcNow.AddHours(5), DateTime.UtcNow.AddHours(8));
            // Act
            await _repository.AddAsync(EmpTask);
            // Assert
            Assert.Contains(EmpTask, _dbContext.EmployeeTasks);
        }

        [Fact]
        public async Task DeleteAsync_EmployeeTask_ShouldNotContain()
        {
            // Act
            Assert.Contains(_data[0], _dbContext.EmployeeTasks);
            await _repository.DeleteAsync(_data[0]);
            Thread.Sleep(100);
            // Assert
            Assert.DoesNotContain(_data[0], _dbContext.EmployeeTasks);
        }

        [Fact]
        public async Task Update_EmployeeTask_ShouldChangeTitle()
        {
            // Arrange
            var EmpTask = await _repository.GetByIdAsync(_empTaskGuid1);
            string newTitle = "ChangedTitle";
            EmpTask.SetTitle(newTitle);
            // Act
            await _repository.UpdateAsync(EmpTask);
            var updatedEmpTask = _dbContext.EmployeeTasks.First(x => x.Id == EmpTask.Id);
            // Assert
            Assert.Equal(newTitle, updatedEmpTask._Title);
        }

        [Fact]
        public async Task Update_EmployeeTask_ShouldChangeDescription()
        {
            // Arrange
            var EmpTask = await _repository.GetByIdAsync(_empTaskGuid1);
            string newDesc = "ChangedDesc";
            EmpTask.SetDescription(newDesc);
            // Act
            await _repository.UpdateAsync(EmpTask);
            var updatedEmpTask = _dbContext.EmployeeTasks.First(x => x.Id == EmpTask.Id);
            // Assert
            Assert.Equal(newDesc, updatedEmpTask._Description);
        }

        [Fact]
        public async Task Update_EmployeeTask_ShouldRemoveUser()
        {
            // Arrange
            var EmpTask = await _repository.GetByIdAsync(_empTaskGuid1);
            EmpTask.RemoveEmployee(_user1);
            // Act
            await _repository.UpdateAsync(EmpTask);
            var updatedEmpTask = _dbContext.EmployeeTasks.First(x => x.Id == EmpTask.Id);
            // Assert
            Assert.Single(updatedEmpTask._AssignedEmployees);
        }

        [Fact]
        public async Task Update_EmployeeTask_ShouldChangePriority()
        {
            // Arrange
            var EmpTask = await _repository.GetByIdAsync(_empTaskGuid1);
            EmpTask.SetPriority(EmployeeTaskPriorityEnum.Urgent);
            // Act
            await _repository.UpdateAsync(EmpTask);
            var updatedEmpTask = _dbContext.EmployeeTasks.First(x => x.Id == EmpTask.Id);
            // Assert
            Assert.Equal(EmployeeTaskPriorityEnum.Urgent, updatedEmpTask._Priority);
        }

        [Fact]
        public async Task Update_EmployeeTask_ShouldChangeStatus()
        {
            // Arrange
            var EmpTask = await _repository.GetByIdAsync(_empTaskGuid1);
            EmpTask.UpdateStatus(EmployeeTaskStatusEnum.InProgress);
            // Act
            await _repository.UpdateAsync(EmpTask);
            var updatedEmpTask = _dbContext.EmployeeTasks.First(x => x.Id == EmpTask.Id);
            // Assert
            Assert.Equal(EmployeeTaskStatusEnum.InProgress, updatedEmpTask._TaskStatus);
        }
    }
}