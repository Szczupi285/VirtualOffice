using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Services;
using VirtualOffice.Domain.ValueObjects.EmployeeTask;

namespace DomainUnitTests
{
    public class EmployeeTaskServiceUnitTests
    {
        private EmployeeTaskService service {  get; set; }
        
        private EmployeeTask _Task1 { get; set; }
        private EmployeeTask _Task2 { get; set; }
        private EmployeeTask _Task3 { get; set; }

        private ApplicationUser _ApplicationUser1 { get; set; }
        private ApplicationUser _ApplicationUser2 { get; set; }
        private ApplicationUser _ApplicationUser3 { get; set; }

        public EmployeeTaskServiceUnitTests()
        {

            ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "NameOne", "SurnameOne");
            ApplicationUser user2 = new ApplicationUser(Guid.NewGuid(), "NameTwo", "SurnameTwo");
            ApplicationUser user3 = new ApplicationUser(Guid.NewGuid(), "NameThree", "SurnameThree");
            _ApplicationUser1 = user1;
            _ApplicationUser2 = user2;
            _ApplicationUser3 = user3;
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>() {user1, user2 };

            // Intance 1
            Guid task1Guid = Guid.NewGuid();
            var task1Id = new EmployeeTaskId(task1Guid);
            var task1Title = new EmployeeTaskTitle("Task 1");
            var task1Description = new EmployeeTaskDescription("Description for Task 1");
            var assignedEmployees1 = applicationUsers;
            var priority1 = EmployeeTaskPriorityEnum.High;
            var startDate1 = new EmployeeTaskStartDate(DateTime.UtcNow);
            var endDate1 = new EmployeeTaskEndDate(DateTime.UtcNow.AddDays(7));

            _Task1 = new EmployeeTask(task1Id, task1Title, task1Description, assignedEmployees1, priority1, startDate1, endDate1);

            // Instance 2
            Guid task2Guid = Guid.NewGuid();
            var task2Id = new EmployeeTaskId(task2Guid);
            var task2Title = new EmployeeTaskTitle("Task 2");
            var task2Description = new EmployeeTaskDescription("Description for Task 2");
            var assignedEmployees2 = applicationUsers;
            var priority2 = EmployeeTaskPriorityEnum.Medium;
            var startDate2 = new EmployeeTaskStartDate(DateTime.UtcNow);
            var endDate2 = new EmployeeTaskEndDate(DateTime.UtcNow.AddDays(5));

            _Task2 = new EmployeeTask(task2Id, task2Title, task2Description, assignedEmployees2, priority2, startDate2, endDate2);

            // Instance 3
            Guid task3Guid = Guid.NewGuid();
            var task3Id = new EmployeeTaskId(task3Guid);
            var task3Title = new EmployeeTaskTitle("Task 3");
            var task3Description = new EmployeeTaskDescription("Description for Task 3");
            var assignedEmployees3 = applicationUsers;
            var priority3 = EmployeeTaskPriorityEnum.Low;
            var startDate3 = new EmployeeTaskStartDate(DateTime.UtcNow);
            var endDate3 = new EmployeeTaskEndDate(DateTime.UtcNow.AddDays(3));

            _Task3 = new EmployeeTask(task3Id, task3Title, task3Description, assignedEmployees3, priority3, startDate3, endDate3);
            

            service = new EmployeeTaskService(new HashSet<EmployeeTask> {_Task1, _Task2});
            
        }

        [Fact]
        public void AssignTask_AddsTaskToList()
        {
            service.AssignTask(_Task3);

            Assert.Contains(_Task3, service._EmployeeTasks);
        }

        [Fact]
        public void AssignTask_AddsTaskToList_ShouldReturnTrue()
        {
            Assert.True(service.AssignTask(_Task3));
        }
        [Fact]
        public void AssignTask_AddsTaskToList_ShouldReturnFalse()
        {
            Assert.False(service.AssignTask(_Task1));
        }

        [Fact]
        public void DeleteTask_RemovesTaskFromList()
        {
            service.DeleteTask(_Task1);

            Assert.DoesNotContain(_Task1, service._EmployeeTasks); 
        }
        [Fact]
        public void DeleteTask_RemovesTaskFromList_ShouldReturnTrue()
        {
            Assert.True(service.DeleteTask(_Task1));
        }
        [Fact]
        public void DeleteTask_RemovesTaskFromList_ShouldReturnFalse()
        {
            Assert.False(service.DeleteTask(_Task3));
        }

        [Fact]
        public void GetTaskById_ReturnsTaskIfExists()
        {
            var resultTask = service.GetTaskById(_Task1.Id);

            Assert.Equal(_Task1, resultTask);
        }

        [Fact]
        public void GetTaskById_ReturnsNullForNonExistentId()
        {
            var resultTask = service.GetTaskById(Guid.NewGuid());

            Assert.Null(resultTask);
        }

        [Fact]
        public void GetAllEmployeeTasks_ReturnsExpectedTasksForUser()
        {
            var resultTasks = service.GetAllEmployeeTasks(_ApplicationUser1);

            Assert.True(resultTasks.Contains(_Task1) && resultTasks.Contains(_Task2) && !resultTasks.Contains(_Task3));
        }

        [Fact]
        public void GetAllEmployeeTasks_ReturnsEmptySetForNonAssignedUser()
        {
            var resultTasks = service.GetAllEmployeeTasks(_ApplicationUser3);

            Assert.Empty(resultTasks);
        }
    }
}
